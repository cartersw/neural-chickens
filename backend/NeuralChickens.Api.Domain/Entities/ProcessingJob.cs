using NeuralChickens.Api.Common.Enums;

namespace NeuralChickens.Api.Domain.Entities
{
    public class ProcessingJob
    {
        public int Id {get; set;}
        public JobType JobType { get; set; }
        public JobStatus JobStatus { get; set; }
        public Guid? ProcessClaimId { get; set; }
        public DateTime? ProcessLeaseExpiresAt { get; set; }
    }
}
