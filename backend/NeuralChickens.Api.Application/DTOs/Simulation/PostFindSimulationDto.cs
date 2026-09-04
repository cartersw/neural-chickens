using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NeuralChickens.Api.Application.DTOs.Simulation
{
    public class PostFindSimulationDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public float Speed { get; set; }
    }
}
