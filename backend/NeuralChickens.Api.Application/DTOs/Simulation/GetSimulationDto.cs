using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Application.DTOs.Simulation
{
    public record GetSimulationDto
    {
        public string Name { get; init; } = string.Empty;
        public int Id { get; init; }
        public string SimulationType { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public int Contestants { get; set; }
        public DateTime RequestedAt { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? StartedAt { get; init; }
        public DateTime? CompletedAt { get; init; }
        
    }
}
