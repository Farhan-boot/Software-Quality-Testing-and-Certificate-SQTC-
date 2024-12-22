using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.HardwareTestings;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureHardwareTesting(this ModelBuilder builder)
        {
            builder.Entity<HardwareTesting>(ac =>
            {
                ac.ToTable("HardwareTesting", "HT");
            });
            builder.Entity<HardwareTesting>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<HardwareTesting>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
