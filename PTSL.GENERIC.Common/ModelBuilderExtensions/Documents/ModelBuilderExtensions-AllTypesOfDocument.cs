using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureAllTypesOfDocument(this ModelBuilder builder)
        {
            builder.Entity<AllTypesOfDocument>(ac =>
            {
                ac.ToTable("AllTypesOfDocument", "GS");
            });
            builder.Entity<AllTypesOfDocument>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
