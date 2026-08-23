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
    }
}
