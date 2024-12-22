using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigurePaymentCalculationRow(this ModelBuilder builder)
        {
            builder.Entity<PaymentCalculationRow>(ac =>
            {
                ac.ToTable("PaymentCalculationRow", "ProjectPackageConfiguration");

                ac.Property(x => x.UnitPrice)
                 .IsRequired(false);
                ac.Property(x => x.NumberOfPackage)
                .IsRequired(false);
                ac.Property(x => x.TotalPrice)
               .IsRequired(false);
                ac.Property(x => x.Vat)
              .IsRequired(false);
                ac.Property(x => x.Tax)
             .IsRequired(false);

            });

            builder.Entity<PaymentCalculationRow>()
               .HasOne(x => x.PaymentCalculationHeader)
               .WithMany(x => x.PaymentCalculationRows)
               .HasForeignKey(x => x.PaymentCalculationHeaderId)
               .IsRequired();

            builder.Entity<PaymentCalculationRow>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
