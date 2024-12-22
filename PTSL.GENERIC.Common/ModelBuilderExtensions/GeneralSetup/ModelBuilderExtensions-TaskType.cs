using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.GeneralSetup;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureTaskType(this ModelBuilder builder)
        {
            builder.Entity<TaskType>(ac =>
            {
                ac.ToTable("TaskType", "GS");

                ac.Property(a => a.TaskTypeName)
                    .HasColumnName("TaskTypeName")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
            });
            builder.Entity<TaskType>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<TaskType>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
