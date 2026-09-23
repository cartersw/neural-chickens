using NeuralChickens.Api.Application.DTOs.Training;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Application.Interfaces.Training
{
    public interface ITrainingJobStore
    {
        Task<TrainingJobClaimDto?> TryClaimNextFindAsync(Guid claimId,
        TimeSpan leaseDuration,
        CancellationToken cancellationToken = default);
    }
}
