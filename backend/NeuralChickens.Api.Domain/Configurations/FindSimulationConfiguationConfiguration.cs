using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralChickens.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Domain.Configurations
{
    public class FindSimulationConfiguationConfiguration : IEntityTypeConfiguration<FindSimulationConfiguration>
    {
        public void Configure(EntityTypeBuilder<FindSimulationConfiguration> builder)
        {
            
        }
    }
}
