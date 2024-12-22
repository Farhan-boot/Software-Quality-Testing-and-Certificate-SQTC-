using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.GeneralSetup;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureCategory(this ModelBuilder builder)
        {
            builder.Entity<Category>(ac =>
            {
                ac.ToTable("Category", "GS");

                ac.Property(a => a.CategoryName)
                    .HasColumnName("CategoryName")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
            });
            builder.Entity<Category>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<Category>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
