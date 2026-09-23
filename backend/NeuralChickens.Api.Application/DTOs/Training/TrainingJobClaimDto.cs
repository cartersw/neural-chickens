using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Application.DTOs.Training
{
    public record TrainingJobClaimDto
    {
        public int SimulationId { get; init; }
        public Guid ClaimId { get; init; }
        public DateTime TrainingLeaseExpiresAt { get; init; }
    }
}
