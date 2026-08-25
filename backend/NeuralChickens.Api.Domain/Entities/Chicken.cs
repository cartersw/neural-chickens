using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Domain.Entities
{
    public class Chicken
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Wins { get; set; }
    }
}
