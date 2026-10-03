using NeuralChickens.Api.Application.Interfaces.Training;
using NeuralChickens.Api.Domain;
using Microsoft.EntityFrameworkCore;
using NeuralChickens.Api.Application.DTOs.Training;
using NeuralChickens.Api.Common.Enums;

public class BrainProcessingJobStore(NeuralChickensDbContext context)
    : IBrainProcessingJobStore
{
    public Task<BrainProcessingJobClaimDto?> TryClaimNextAsync(
        SimulationType simululationType, 
        Guid claimId, 
        TimeSpan leaseDuration, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}