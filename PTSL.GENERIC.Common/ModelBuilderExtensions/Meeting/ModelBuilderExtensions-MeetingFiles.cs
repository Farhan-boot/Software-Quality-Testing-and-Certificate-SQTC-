using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.Project;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureMeetingFiles(this ModelBuilder builder)
        {
            builder.Entity<MeetingFiles>(ac =>
            {
                ac.ToTable("MeetingFiles", "GS");
            });
            builder.Entity<MeetingFiles>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            
            //builder.Entity<AttendedUserMeeting>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);
        }

    }
}
