using System.ComponentModel.DataAnnotations;

namespace NeuralChickens.Api.Domain.Entities
{
    public class SimulationRenderJobDetails
    {
        public int ProcessingJobId { get; set; }
        [Required]
        public ProcessingJob ProcessingJob { get; set; }

    }
}
