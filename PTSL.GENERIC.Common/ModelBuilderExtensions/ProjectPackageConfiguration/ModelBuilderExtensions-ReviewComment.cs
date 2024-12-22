using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureReviewComment(this ModelBuilder builder)
        {
            builder.Entity<ReviewComment>(ac =>
            {
                ac.ToTable("ReviewComment", "ProjectPackageConfiguration");
            });
            builder.Entity<ReviewComment>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
