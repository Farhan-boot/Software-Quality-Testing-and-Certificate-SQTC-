using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.GeneralSetup;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureMeetingType(this ModelBuilder builder)
        {
            builder.Entity<MeetingType>(ac =>
            {
                ac.ToTable("MeetingType", "GS");

                ac.Property(a => a.MeetingTypeName)
                    .HasColumnName("MeetingTypeName")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
            });
            builder.Entity<MeetingType>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<MeetingType>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
