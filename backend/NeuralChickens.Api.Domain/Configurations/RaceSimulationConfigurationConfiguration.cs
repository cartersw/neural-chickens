using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralChickens.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Domain.Configurations
{
    public class RaceSimulationConfigurationConfiguration : IEntityTypeConfiguration<RaceSimulationConfiguration>
    {
        public void Configure(EntityTypeBuilder<RaceSimulationConfiguration> builder)
        {
            builder.HasKey(c => c.SimulationId);

            builder.HasOne(c => c.Simulation)
                .WithOne()
                .HasForeignKey<RaceSimulationConfiguration>(c => c.SimulationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
