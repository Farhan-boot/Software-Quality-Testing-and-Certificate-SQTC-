using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.Entity;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions;

public static partial class EntityModelBuilderExtensions
{
    public static void ConfigureUserRole(this ModelBuilder builder)
    {
        builder.Entity<UserRole>(ac =>
        {
            ac.ToTable("UserRoles", "System");

            ac.Property(a => a.RoleName)
                .HasColumnName("RoleName")
                .HasColumnType("varchar(500)")
                .IsRequired();

            ac.Property(a => a.AccessList)
                .HasColumnType("varchar(900)")
                .IsRequired();
        });

        builder.Entity<UserRole>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
    }
}
