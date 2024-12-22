using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureProjectPricingSetup(this ModelBuilder builder)
        {
            builder.Entity<ProjectPricingSetup>(ac =>
            {
                ac.ToTable("ProjectPricingSetup", "ProjectPackageConfiguration");

                ac.Property(x => x.Price)
                 .IsRequired();
            });

            builder.Entity<ProjectPricingSetup>()
               .HasOne(x => x.ProjectModuleName)
               .WithMany(x => x.ProjectPricingSetup)
               .HasForeignKey(x => x.ProjectModuleNameId)
               .IsRequired();

            builder.Entity<ProjectPricingSetup>()
            .HasOne(x => x.ProjectPackage)
            .WithMany(x => x.ProjectPricingSetup)
            .HasForeignKey(x => x.ProjectPackageId)
            .IsRequired();

           // builder.Entity<ProjectPricingSetup>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
