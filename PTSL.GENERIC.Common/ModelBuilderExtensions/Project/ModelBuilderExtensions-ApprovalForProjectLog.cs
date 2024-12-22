using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureApprovalForProjectLog(this ModelBuilder builder)
        {
            builder.Entity<ApprovalForProjectLog>(ac =>
            {
                ac.ToTable("ApprovalForProjectLog", "GS");
            });
            builder.Entity<ApprovalForProjectLog>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<ApprovalForProjectLog>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
