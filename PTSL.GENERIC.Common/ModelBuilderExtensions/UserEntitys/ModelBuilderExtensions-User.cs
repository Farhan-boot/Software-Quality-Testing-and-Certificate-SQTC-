using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.Entity;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions;

public static partial class EntityModelBuilderExtensions
{
    public static void ConfigureUser(this ModelBuilder builder)
    {
        builder.Entity<User>(ac =>
        {
            ac.ToTable("User", "System");

            ac.Property(a => a.UserName)
                .HasColumnName("UserName")
                .HasColumnType("varchar(500)")
                .IsRequired();

            ac.Property(a => a.UserEmail)
                .HasColumnName("UserEmail")
                .HasColumnType("varchar(100)")
                .IsRequired();

            ac.Property(a => a.UserPassword)
                .HasColumnName("UserPassword")
                .HasColumnType("varchar(500)")
                .IsRequired();

            ac.Property(a => a.UserPhone)
                .HasColumnName("UserPhone")
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            ac.Property(a => a.UserGroup)
                .HasColumnName("UserGroup")
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            ac.Property(a => a.UserStatus)
                .HasColumnName("UserStatus")
                .HasColumnType("int")
                .IsRequired();

            ac.Property(a => a.RoleName)
                .HasColumnName("RoleName")
                .HasColumnType("varchar(100)")
                .IsRequired(false);
        });

        builder.Entity<User>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        builder.Entity<User>()
            .HasOne(x => x.Client)
            .WithMany(x => x.ClientUsers)
            .HasForeignKey(x => x.ClientId);
    }
}
