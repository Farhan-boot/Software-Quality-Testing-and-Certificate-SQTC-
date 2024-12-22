using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureFeedback(this ModelBuilder builder)
        {
            builder.Entity<Feedback>(ac =>
            {
                ac.ToTable("Feedback", "ProjectPackageConfiguration");
            });
            builder.Entity<Feedback>().HasQueryFilter(q => !q.IsDeleted);
            //&& q.IsActive
        }

    }
}
