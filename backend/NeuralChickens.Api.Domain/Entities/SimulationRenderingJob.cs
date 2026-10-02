using NeuralChickens.Api.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace NeuralChickens.Api.Domain.Entities
{
    public class SimulationRenderingJob
    {
        public int Id { get; set; }
        public JobStatus JobStatus { get; set; }
        public Guid? SimulationProcessClaimId { get; set; }
        public DateTime? SimulationProcessLeaseExpiresAt { get; set; }

    }
}
