using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigurePaymentCalculationHeader(this ModelBuilder builder)
        {
            builder.Entity<PaymentCalculationHeader>(ac =>
            {
                ac.ToTable("PaymentCalculationHeader", "ProjectPackageConfiguration");
            });
            builder.Entity<PaymentCalculationHeader>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
