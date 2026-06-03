using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(i => i.Name)
            .IsUnique();

        builder.Property(p => p.Name)
            .HasMaxLength(150)
            .IsRequired();
    }
}