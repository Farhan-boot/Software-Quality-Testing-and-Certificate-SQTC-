using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigurePaymentInformation(this ModelBuilder builder)
        {
            builder.Entity<PaymentInformation>(ac =>
            {
                ac.ToTable("PaymentInformation", "ProjectPackageConfiguration");
            });

            builder.Entity<PaymentInformation>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
