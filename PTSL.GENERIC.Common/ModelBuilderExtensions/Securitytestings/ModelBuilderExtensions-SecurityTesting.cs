using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.SecurityTestings;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureSecurityTesting(this ModelBuilder builder)
        {
            builder.Entity<SecurityTesting>(ac =>
            {
                ac.ToTable("SecurityTesting", "ST");
            });
            builder.Entity<SecurityTesting>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<SecurityTesting>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
