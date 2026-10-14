using System;
using System.Collections.Generic;
using System.Text;
using NeuralChickens.Api.Common.Enums;

namespace NeuralChickens.Api.Application.DTOs.Training
{
    public record BrainProcessingJobClaimDto
    {
        public int BrainProcessingJobId { get; set; }
        public int ChickenId { get; set; }
        public SimulationType SimulationType { get; init; }
        public Guid ClaimId { get; init; }
        public DateTime LeaseExpiresAt { get; init; }
    }
   
}
