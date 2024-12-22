using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.GeneralSetup;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureDocumentCategories(this ModelBuilder builder)
        {
            builder.Entity<DocumentCategories>(ac =>
            {
                ac.ToTable("DocumentCategories", "GS");

                ac.Property(a => a.Name)
                    .HasColumnName("Name")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
            });
            builder.Entity<DocumentCategories>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<DocumentCategories>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
