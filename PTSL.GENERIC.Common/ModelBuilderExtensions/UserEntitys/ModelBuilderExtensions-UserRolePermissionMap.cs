using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.Entity.UserEntitys;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions;

public static partial class EntityModelBuilderExtensions
{
    public static void ConfigureUserRolePermissionMap(this ModelBuilder builder)
    {
        builder.Entity<UserRolePermissionMap>(ac =>
        {
            ac.ToTable(nameof(UserRolePermissionMap), "System");

            ac.Property(a => a.UserRoleId)
                .IsRequired();
        });

        builder.Entity<UserRolePermissionMap>()
            .HasOne(x => x.UserRole)
            .WithMany(x => x.UserRolePermissionMaps)
            .HasForeignKey(x => x.UserRoleId)
            .IsRequired();

        builder.Entity<UserRolePermissionMap>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
    }
}
