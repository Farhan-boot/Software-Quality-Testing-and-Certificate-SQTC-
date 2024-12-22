using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureReconciliation(this ModelBuilder builder)
        {
            builder.Entity<Reconciliation>(ac =>
            {
                ac.ToTable("Reconciliation", "ProjectPackageConfiguration");

            });

            builder.Entity<Reconciliation>()
               .HasOne(x => x.PaymentInformation)
               .WithMany(x => x.Reconciliations)
               .HasForeignKey(x => x.PaymentInformationId)
               .IsRequired();

            builder.Entity<Reconciliation>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
