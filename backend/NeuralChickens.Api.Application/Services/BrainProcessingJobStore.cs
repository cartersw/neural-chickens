using NeuralChickens.Api.Application.Interfaces.Training;
using NeuralChickens.Api.Domain;
using Microsoft.EntityFrameworkCore;
using NeuralChickens.Api.Application.DTOs.Training;
using NeuralChickens.Api.Common.Enums;
using System.Data;

public class BrainProcessingJobStore(NeuralChickensDbContext context)
    : IBrainProcessingJobStore
{
    public async Task<BrainProcessingJobClaimDto?> TryClaimNextAsync(
        SimulationType simulationType, 
        Guid claimId, 
        TimeSpan leaseDuration, 
        CancellationToken cancellationToken = default)
    {
        if (!Enum.IsDefined(simulationType))
        {
            throw new ArgumentOutOfRangeException(nameof(simulationType));
        }

        if(claimId == Guid.Empty)
        {
            throw new ArgumentException(
                "A claim ID is required.",
                nameof(claimId
                ));
        }

        if (leaseDuration <= TimeSpan.Zero || leaseDuration.TotalSeconds > int.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(leaseDuration));
        }

        var leaseSeconds = (int)Math.Ceiling(leaseDuration.TotalSeconds);

        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction =
                await context.Database.BeginTransactionAsync(
                    IsolationLevel.ReadCommitted,
                    cancellationToken);

            var claims = await context.Database
                .SqlQuery<BrainProcessingJobClaimDto>($"""
                    SET NOCOUNT ON;

                    DECLARE @ClaimId uniqueidentifier = {claimId};
                    DECLARE @SimulationType int = {(int)simulationType};
                    DECLARE @Requested int = {(int)JobStatus.Requested};
                    DECLARE @InProgress int = {(int)JobStatus.InProgress};
                    DECLARE @LeaseSeconds int = {leaseSeconds};

                    -- A previous attempt may have committed even if
                    -- the worker never received its response.
                    IF EXISTS
                    (
                        SELECT 1
                        FROM dbo.BrainProcessingJobs WITH (UPDLOCK)
                        WHERE BrainProcessingClaimId = @ClaimId
                    )
                    BEGIN
                        IF NOT EXISTS
                        (
                            SELECT 1
                            FROM dbo.BrainProcessingJobs
                            WHERE BrainProcessingClaimId = @ClaimId
                              AND SimulationType = @SimulationType
                              AND JobStatus = @InProgress
                              AND BrainProcessLeaseExpiresAt > SYSUTCDATETIME()
                        )
                        BEGIN
                            THROW 50001,
                                'This claim ID no longer represents a valid active claim.',
                                1;
                        END;

                        SELECT
                            Id AS BrainProcessingJobId,
                            ChickenId,
                            SimulationType,
                            BrainProcessingClaimId AS ClaimId,
                            BrainProcessLeaseExpiresAt AS LeaseExpiresAt
                        FROM dbo.BrainProcessingJobs
                        WHERE BrainProcessingClaimId = @ClaimId;
                    END
                    ELSE
                    BEGIN
                        ;WITH NextJob AS
                        (
                            SELECT TOP (1) *
                            FROM dbo.BrainProcessingJobs
                                WITH (UPDLOCK, READPAST, READCOMMITTEDLOCK)
                            WHERE SimulationType = @SimulationType
                              AND JobStatus = @Requested
                              AND BrainProcessingClaimId IS NULL
                            ORDER BY Id
                        )
                        UPDATE NextJob
                        SET
                            JobStatus = @InProgress,
                            BrainProcessingClaimId = @ClaimId,
                            BrainProcessLeaseExpiresAt =
                                DATEADD(
                                    SECOND,
                                    @LeaseSeconds,
                                    SYSUTCDATETIME()
                                )
                        OUTPUT
                            inserted.Id AS BrainProcessingJobId,
                            inserted.ChickenId,
                            inserted.SimulationType,
                            inserted.BrainProcessingClaimId AS ClaimId,
                            inserted.BrainProcessLeaseExpiresAt AS LeaseExpiresAt;
                    END;
                    """)
                .ToListAsync(cancellationToken);

            var claim = claims.SingleOrDefault();


            if(claim is not null)
            {
                claim = claim with
                {
                    LeaseExpiresAt = DateTime.SpecifyKind(
                        claim.LeaseExpiresAt,
                        DateTimeKind.Utc)
                };
            }

            await transaction.CommitAsync(cancellationToken);


            return claim;
        });

    }
}