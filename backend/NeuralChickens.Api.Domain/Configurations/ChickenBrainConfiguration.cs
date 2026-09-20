using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralChickens.Api.Domain.Entities;

namespace NeuralChickens.Api.Domain.Configurations
{
    public class ChickenBrainConfiguration : IEntityTypeConfiguration<ChickenBrain>
    {
        public void Configure(EntityTypeBuilder<ChickenBrain> builder)
        {

            builder.HasKey(b => b.Id);
            builder.Property(b => b.BrainPath).IsRequired();
            builder.Property(b => b.SimulationType).IsRequired();
            builder.Property(b => b.CreatedAt).IsRequired();


            builder.HasOne(b => b.Chicken)
                .WithMany(c => c.ChickenBrains)
                .HasForeignKey(b => b.ChickenId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(b => new { b.ChickenId, b.SimulationType, b.CreatedAt });

        }
    }
}
