using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralChickens.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Domain.Configurations
{
    public class FindSimulationResultConfiguration : IEntityTypeConfiguration<FindSimulationResult>
    {
        public void Configure(EntityTypeBuilder<FindSimulationResult> builder)
        {
            builder.HasKey(c => c.SimulationId);

            builder.HasOne(c => c.Simulation)
                .WithOne()
                .HasForeignKey<FindSimulationResult>(c => c.SimulationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
