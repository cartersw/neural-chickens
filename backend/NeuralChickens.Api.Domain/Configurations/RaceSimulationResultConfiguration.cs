using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralChickens.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Domain.Configurations
{
    public class RaceSimulationResultConfiguration : IEntityTypeConfiguration<RaceSimulationResult>
    {
        public void Configure(EntityTypeBuilder<RaceSimulationResult> builder)
        {
            builder.HasKey(c => c.SimulationId);

            builder.HasOne(c => c.Simulation)
                .WithOne()
                .HasForeignKey<RaceSimulationResult>(c => c.SimulationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
