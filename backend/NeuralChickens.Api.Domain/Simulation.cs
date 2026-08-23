using NeuralChickens.Api.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NeuralChickens.Api.Domain
{
    public class Simulation
    {
        public int Id { get; set; }
        [Required]
        public SimulationType SimulationType { get; set; }
        [Required]
        public SimulationStatus SimulationStatus { get; set; }

        public DateTime RequestedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string VideoPath { get; set; } = string.Empty;
        
    }
}
