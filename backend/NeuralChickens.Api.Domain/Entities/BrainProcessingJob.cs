using NeuralChickens.Api.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace NeuralChickens.Api.Domain.Entities
{
    public class BrainProcessingJob
    {
        public int Id {get; set;}
        public JobStatus JobStatus { get; set; }
        public Guid? BrainProcessingClaimId { get; set; }
        public DateTime? BrainProcessLeaseExpiresAt { get; set; }
        public int ChickenId { get; set; }
        public Chicken? Chicken { get; set; }
        [Required]
        public SimulationType SimulationType { get; set; }
    }
}
