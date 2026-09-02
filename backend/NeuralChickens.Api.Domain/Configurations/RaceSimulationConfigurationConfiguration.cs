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

        }
    }
}
