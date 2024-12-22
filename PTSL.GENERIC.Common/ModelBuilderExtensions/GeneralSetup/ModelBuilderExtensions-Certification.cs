using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.GeneralSetup;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureCertification(this ModelBuilder builder)
        {
            builder.Entity<Certification>(ac =>
            {
                ac.ToTable("Certification", "GS");

                ac.Property(a => a.CertificationName)
                    .HasColumnName("CertificationName")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
                ac.Property(a => a.VendorName)
                    .HasColumnName("VendorName")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
            });
            builder.Entity<Certification>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<Certification>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
