using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NeuralChickens.Api.Domain.Entities
{
    public class FindSimulationConfiguration
    {
        public int SimulationId { get; set; }
        public Simulation Simulation { get; set; } = null!;

        [Required]
        public float Speed { get; set; } 
    }
}
