using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.GeneralSetup;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureDesignation(this ModelBuilder builder)
        {
            builder.Entity<Designation>(ac =>
            {
                ac.ToTable("Designation", "GS");

                ac.Property(a => a.Name)
                    .HasColumnName("Name")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
            });
            builder.Entity<Designation>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<Designation>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
