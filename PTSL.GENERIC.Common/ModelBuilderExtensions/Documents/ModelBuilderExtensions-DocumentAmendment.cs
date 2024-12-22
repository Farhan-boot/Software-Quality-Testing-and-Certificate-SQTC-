using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureDocumentAmendment(this ModelBuilder builder)
        {
            builder.Entity<DocumentAmendment>(ac =>
            {
                ac.ToTable("DocumentAmendment", "GS");
            });
            builder.Entity<DocumentAmendment>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
