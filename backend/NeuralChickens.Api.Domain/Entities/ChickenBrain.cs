using NeuralChickens.Api.Common.Enums;

namespace NeuralChickens.Api.Domain.Entities
{
    public class ChickenBrain
    {
        public int Id { get; set; }
        public SimulationType SimulationType { get; set; }
        public string BrainPath { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int ChickenId { get; set; }
        public Chicken Chicken { get; set; } = null!;
    }
}
