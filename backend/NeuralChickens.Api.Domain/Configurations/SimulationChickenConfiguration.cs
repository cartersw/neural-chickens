using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralChickens.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Domain.Configurations
{
    public class SimulationChickenConfiguration : IEntityTypeConfiguration<SimulationChicken>
    {
        public void Configure(EntityTypeBuilder<SimulationChicken> builder)
        {
            builder.HasKey(sc => new { sc.SimulationId, sc.ChickenId });

            builder.HasOne(sc => sc.Simulation)
                .WithMany(s => s.SimulationChickens)
                .HasForeignKey(sc => sc.SimulationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sc => sc.Chicken)
                .WithMany()
                .HasForeignKey(sc => sc.ChickenId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
