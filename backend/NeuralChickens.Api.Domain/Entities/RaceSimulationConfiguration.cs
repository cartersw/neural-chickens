using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NeuralChickens.Api.Domain.Entities
{
    public class RaceSimulationConfiguration
    {
        public int SimulationId { get; set; }
        public Simulation Simulation { get; set; } = null!;
    }
}
