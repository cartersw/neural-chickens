using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralChickens.Api.Domain.Entities;

namespace NeuralChickens.Api.Domain.Configurations
{
    public class BrainProcessingJobConfiguration : IEntityTypeConfiguration<BrainProcessingJob>
    {
        public void Configure(EntityTypeBuilder<BrainProcessingJob> builder)
        {
            builder.HasIndex(job => new
            {
                job.SimulationType,
                job.JobStatus,
                job.Id
            });

            builder.HasIndex(job => job.BrainProcessingClaimId)
                .IsUnique()
                .HasFilter("[BrainProcessingClaimId] IS NOT NULL");
        }
    }
}
