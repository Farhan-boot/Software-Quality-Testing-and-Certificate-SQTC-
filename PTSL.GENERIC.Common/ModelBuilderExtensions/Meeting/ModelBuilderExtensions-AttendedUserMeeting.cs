using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.Project;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureAttendedUserMeeting(this ModelBuilder builder)
        {
            builder.Entity<AttendedUserMeeting>(ac =>
            {
                ac.ToTable("AttendedUserMeeting", "GS");
            });
            builder.Entity<AttendedUserMeeting>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            builder.Entity<AttendedUserMeeting>()
                .HasOne<User>(s => s.AttendUser)
                .WithMany(s => s.AttendedUserMeeting)
                .HasForeignKey(s => s.AttendeduserId).OnDelete(DeleteBehavior.ClientCascade);
            //builder.Entity<AttendedUserMeeting>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);
        }

    }
}
