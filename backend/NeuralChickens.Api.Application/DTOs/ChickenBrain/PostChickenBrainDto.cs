using NeuralChickens.Api.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NeuralChickens.Api.Application.DTOs.ChickenBrain
{
    public class PostChickenBrainDto
    {
        [Required]
        public int ChickenId { get; set; }

        [Required]
        public SimulationType SimulationType { get; set; }

    }
}
