using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Domain.Entities
{
    public class RaceSimulationResult
    {
        public int SimulationId { get; set; }

        public int WinnerChickenId { get; set; }

        public Simulation Simulation { get; set; } = null!;

        public Chicken Chicken { get; set; } = null!;
    }
}
