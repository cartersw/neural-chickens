using NeuralChickens.Api.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace NeuralChickens.Api.Domain.Entities
{
    public class BrainProcessingJob
    {
        public int Id {get; set;}
        public JobStatus JobStatus { get; set; }
        public Guid? BrainProcessClaimId { get; set; }
        public DateTime? BrainProcessLeaseExpiresAt { get; set; }
        [Required]
        public int ChickenId { get; set; }
        public Chicken Chicken { get; set; } = null;
        [Required]
        public SimulationType SimulationType { get; set; }
    }
}
