using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Project;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureTestCategory(this ModelBuilder builder)
        {
            builder.Entity<TestCategory>(ac =>
            {
                ac.ToTable("TestCategory", "PR");
            });
            builder.Entity<TestCategory>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }
    }
}
