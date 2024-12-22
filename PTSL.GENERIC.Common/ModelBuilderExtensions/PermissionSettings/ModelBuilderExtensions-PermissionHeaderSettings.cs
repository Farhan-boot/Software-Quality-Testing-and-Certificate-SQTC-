using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigurePermissionHeaderSettings(this ModelBuilder builder)
        {
            builder.Entity<PermissionHeaderSettings>(ac =>
            {
                ac.ToTable("PermissionHeaderSettings", "PermissionSettings");
                //ac.Property(a => a.CommitteeApprovalBy)
                //   .IsRequired(false);
            });

            // Other Info
            builder.Entity<PermissionHeaderSettings>()
           .HasOne(x => x.UserRole)
           .WithMany(x => x.PermissionHeaderSettings)
           .HasForeignKey(x => x.UserRoleId)
           .IsRequired(false);

            builder.Entity<PermissionHeaderSettings>()
             .HasOne(x => x.User)
             .WithMany(x => x.PermissionHeaderSettings)
             .HasForeignKey(x => x.UserId)
             .IsRequired(false);
            builder.Entity<PermissionHeaderSettings>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);

        }
    }
}
