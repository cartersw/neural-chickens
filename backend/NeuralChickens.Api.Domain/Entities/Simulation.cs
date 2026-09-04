using NeuralChickens.Api.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NeuralChickens.Api.Domain.Entities
{
    public class Simulation
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
        [Required]
        public SimulationType SimulationType { get; set; }
        [Required]
        public SimulationStatus SimulationStatus { get; set; }

        [Required]
        public int Contestants { get; set; }

        public DateTime RequestedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string VideoPath { get; set; } = string.Empty;

        public IList<SimulationChicken> SimulationChickens { get; set; } = [];

    }
}
