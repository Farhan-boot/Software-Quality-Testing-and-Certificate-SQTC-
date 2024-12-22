using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigurePermissionRowSettings(this ModelBuilder builder)
        {
            builder.Entity<PermissionRowSettings>(ac =>
            {
                ac.ToTable("PermissionRowSettings", "PermissionSettings");
 
            });

            builder.Entity<PermissionRowSettings>()
              .HasOne(x => x.PermissionHeaderSettings)
              .WithMany(x => x.PermissionRowSettings)
              .HasForeignKey(x => x.PermissionHeaderSettingsId)
              .IsRequired(false);

            builder.Entity<PermissionRowSettings>()
              .HasOne(x => x.UserRole)
              .WithMany(x => x.PermissionRowSettings)
              .HasForeignKey(x => x.UserRoleId)
              .IsRequired(false);

            builder.Entity<PermissionRowSettings>()
               .HasOne(x => x.User)
               .WithMany(x => x.PermissionRowSettings)
               .HasForeignKey(x => x.UserId)
               .IsRequired(false);
            builder.Entity<PermissionRowSettings>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);

        }
    }
}
