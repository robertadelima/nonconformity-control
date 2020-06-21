using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NonconformityControl.Models;

namespace NonconformityControl.Infra.Mapping
{
    public class ActionMap : IEntityTypeConfiguration<Action>
    {
        public void Configure(EntityTypeBuilder<Action> builder)
        {
            builder.ToTable("Actions");
            builder.HasKey("Id");
            builder.Property(p => p.Description).IsRequired().HasMaxLength(1024).HasColumnType("VARCHAR(1024)");
            builder.HasOne(p => p.Nonconformity).WithMany(p => p.Actions);
        }
    }
}