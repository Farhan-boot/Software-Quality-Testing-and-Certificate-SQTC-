using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureProjectModuleName(this ModelBuilder builder)
        {
            builder.Entity<ProjectModuleName>(ac =>
            {
                ac.ToTable("ProjectModuleName", "ProjectPackageConfiguration");
            });
            builder.Entity<ProjectModuleName>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
