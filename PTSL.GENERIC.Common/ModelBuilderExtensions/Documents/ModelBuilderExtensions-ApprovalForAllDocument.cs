using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureApprovalForAllDocument(this ModelBuilder builder)
        {
            builder.Entity<ApprovalForAllDocument>(ac =>
            {
                ac.ToTable("ApprovalForAllDocument", "GS");
            });
            builder.Entity<ApprovalForAllDocument>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
