using System;
using System.Collections.Generic;
using System.Text;
using NeuralChickens.Api.Common.Enums;

namespace NeuralChickens.Api.Application.DTOs.Training
{
    public record TrainingJobClaimDto
    {
        public SimulationType SimulationType { get; init; }
        public int SimulationId { get; init; }
        public Guid ClaimId { get; init; }
        public DateTime TrainingLeaseExpiresAt { get; init; }
    }
}
