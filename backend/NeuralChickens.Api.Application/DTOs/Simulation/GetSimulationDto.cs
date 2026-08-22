using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Application.DTOs.Simulation
{
    public record GetSimulationDto
    {
        public int Id { get; init; }
        public string SimulationType { get; init; }
        public string Status { get; init; }
        public DateTime RequestedAt { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? StartedAt { get; init; }
        public DateTime? CompletedAt { get; init; }
        
    }
}
