using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(i => i.Email)
            .IsUnique();

        builder.Property(p => p.Email)
            .HasMaxLength(250)
            .IsRequired();
        
        builder.Property(p => p.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}