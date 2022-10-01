using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.Users;

namespace PracticalTest.Infrastructure.Users.Configuration;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.MapKey();
        builder.Property(x => x.Password).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
        builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Role).HasMaxLength(100).IsRequired();
    }
}