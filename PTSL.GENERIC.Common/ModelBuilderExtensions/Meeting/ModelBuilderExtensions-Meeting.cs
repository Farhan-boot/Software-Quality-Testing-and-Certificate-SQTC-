using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Meetings;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureMeeting(this ModelBuilder builder)
        {
            builder.Entity<Meeting>(ac =>
            {
                ac.ToTable("Meeting", "GS");
            });
            builder.Entity<Meeting>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
                
            //builder.Entity<Meeting>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
