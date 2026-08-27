using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Domain.Entities
{
    public class FindSimulationResult
    {
        public int SimulationId { get; set; }

        public Simulation Simulation { get; set; } = null!;

        public int Wins { get; set; }
    }
}
