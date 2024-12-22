using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureDepositSlipFile(this ModelBuilder builder)
        {
            builder.Entity<DepositSlipFile>(ac =>
            {
                ac.ToTable("DepositSlipFile", "ProjectPackageConfiguration");
            });

            builder.Entity<DepositSlipFile>()
              .HasOne(x => x.PaymentInformation)
              .WithMany(x => x.DepositSlipFiles)
              .HasForeignKey(x => x.PaymentInformationId)
              .IsRequired(false);

            builder.Entity<DepositSlipFile>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
