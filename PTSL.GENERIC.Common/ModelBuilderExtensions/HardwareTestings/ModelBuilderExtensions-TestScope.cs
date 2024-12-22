using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.HardwareTestings;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureTestScope(this ModelBuilder builder)
        {
            builder.Entity<TestScope>(ac =>
            {
                ac.ToTable("TestScope", "HT");
            });
            builder.Entity<TestScope>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<HardwareTesting>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
