using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureProjectCertification(this ModelBuilder builder)
        {
            builder.Entity<ProjectCertification>(ac =>
            {
                ac.ToTable("ProjectCertification", "GS");
            });
            builder.Entity<ProjectCertification>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
