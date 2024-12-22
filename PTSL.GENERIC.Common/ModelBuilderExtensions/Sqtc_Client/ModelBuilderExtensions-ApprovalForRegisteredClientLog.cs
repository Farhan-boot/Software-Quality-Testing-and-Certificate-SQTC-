using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureApprovalForRegisteredClientLog(this ModelBuilder builder)
        {
            builder.Entity<Entity.Sqtc_ClientLog.ApprovalForRegisteredClientLog>(ac =>
            {
                ac.ToTable("ApprovalForRegisteredClientLog", "GS");
            });
            builder.Entity<ApprovalForRegisteredClientLog>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<ApprovalForRegisteredClientLog>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
