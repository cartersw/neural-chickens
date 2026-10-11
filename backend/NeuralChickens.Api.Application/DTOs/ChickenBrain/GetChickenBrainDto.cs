using NeuralChickens.Api.Common.Enums;
using NeuralChickens.Api.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace NeuralChickens.Api.Application.DTOs.ChickenBrain
{
    public struct GetChickenBrainDto
    {
        public int ChickenId { get; init; }
        public SimulationType SimulationType { get; init; }
        public string BrainPath { get; init; }
        public DateTime CreatedAt { get; init; }
        public int BrainProcessingJobId { get; init; }

    }
}
