using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureDocumentsByType(this ModelBuilder builder)
        {
            builder.Entity<DocumentsByType>(ac =>
            {
                ac.ToTable("DocumentsByType", "GS");
            });
            builder.Entity<DocumentsByType>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
