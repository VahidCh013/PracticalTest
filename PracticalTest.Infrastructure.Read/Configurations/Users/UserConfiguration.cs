using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticalTest.Domain.Read.Users;

namespace PracticalTest.Infrastructure.Read.Configurations.Users;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName).HasMaxLength(100);
        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.Password).HasMaxLength(100);
        builder.Property(x => x.Role).HasMaxLength(100);
    }
}