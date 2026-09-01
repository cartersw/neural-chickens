using Microsoft.EntityFrameworkCore;
using NeuralChickens.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NeuralChickens.Api.Domain
{
    public class NeuralChickensDbContext : DbContext
    {
        public NeuralChickensDbContext(DbContextOptions<NeuralChickensDbContext> options) : base(options)
        {

        }

        public DbSet<Chicken> Chickens { get; set; }
        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<SimulationChicken> SimulationChickens { get; set; }
        public DbSet<RaceSimulationConfiguration> RaceSimulationConfigurations { get; set; }
        public DbSet<RaceSimulationResult> RaceSimulationResults { get; set; }
        public DbSet<FindSimulationConfiguration> FindSimulationConfigurations { get; set; }
        public DbSet<FindSimulationResult> FindSimulationResults { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
