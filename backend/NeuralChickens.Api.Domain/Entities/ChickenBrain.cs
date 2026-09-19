using NeuralChickens.Api.Common.Enums;

namespace NeuralChickens.Api.Domain.Entities
{
    public class ChickenBrain
    {
        public SimulationType SimulationType { get; set; }
        public string BrainPath { get; set; } = string.Empty;
        public int ChickenId { get; set; }
        public Chicken Chicken { get; set; } = null!;
    }
}
