using NeuralChickens.Api.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Application.DTOs.Training
{
    public record SimulationRenderingJobClaimDto
    {
        public SimulationType SimulationType { get; init; }
        public int SimulationId { get; init; }
        public Guid ClaimId { get; init; }
        public DateTime TrainingLeaseExpiresAt { get; init; }
    }
}
