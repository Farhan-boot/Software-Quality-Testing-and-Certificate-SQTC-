using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureProjectPackage(this ModelBuilder builder)
        {
            builder.Entity<ProjectPackage>(ac =>
            {
                ac.ToTable("ProjectPackage", "ProjectPackageConfiguration");

                ac.Property(x => x.PackageName)
                 .HasColumnType("varchar(500)")
                 .IsRequired(false);

                ac.Property(x => x.PackageDescription)
                .HasColumnType("varchar(500)")
                .IsRequired(false);

            });

            builder.Entity<ProjectPackage>()
               .HasOne(x => x.ProjectModuleName)
               .WithMany(x => x.ProjectPackages)
               .HasForeignKey(x => x.ProjectModuleNameId)
               .IsRequired();

            builder.Entity<ProjectPackage>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }

    }
}
