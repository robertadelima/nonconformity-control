using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NonconformityControl.Models;

namespace NonconformityControl.Infra.Mapping
{
    public class NonconformityMap : IEntityTypeConfiguration<Nonconformity>
    {
        public void Configure(EntityTypeBuilder<Nonconformity> builder)
        {
            builder.ToTable("Nonconformities");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Version);
            builder.Property(p => p.Code).HasColumnType("VARCHAR(15)");
            builder.Property(p => p.Description).HasMaxLength(1024).HasColumnType("VARCHAR(1024)");
            builder.Property(p => p.Status).HasColumnType("TINYINT");
            builder.Property(p => p.Evaluation).HasColumnType("TINYINT");
            //builder.HasMany(p => p.Actions).WithOne(p => p.Nonconformity);
        }
    }
}