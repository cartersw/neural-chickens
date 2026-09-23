using NeuralChickens.Api.Application.DTOs.Training;
using NeuralChickens.Api.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Application.Interfaces.Training
{
    public interface ITrainingJobStore
    {
        Task<TrainingJobClaimDto?> TryClaimNextAsync(
            SimulationType simululationType,
            Guid claimId,
            TimeSpan leaseDuration,
            CancellationToken cancellationToken = default);
    }
}
