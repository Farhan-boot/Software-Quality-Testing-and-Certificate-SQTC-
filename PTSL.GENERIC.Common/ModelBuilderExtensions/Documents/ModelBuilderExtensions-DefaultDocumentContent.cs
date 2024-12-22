using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureDefaultDocumentContent(this ModelBuilder builder)
        {
            builder.Entity<DefaultDocumentContent>(ac =>
            {
                ac.ToTable("DefaultDocumentContent", "GS");
            });
            builder.Entity<DefaultDocumentContent>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
