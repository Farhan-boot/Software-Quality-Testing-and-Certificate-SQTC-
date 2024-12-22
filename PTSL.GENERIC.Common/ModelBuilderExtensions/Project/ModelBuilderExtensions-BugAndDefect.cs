using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureBugAndDefect(this ModelBuilder builder)
        {
            builder.Entity<BugAndDefect>(ac =>
            {
                ac.ToTable("BugAndDefect", "PR");
            });
            builder.Entity<BugAndDefect>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            builder.Entity<BugAndDefect>()
                .HasOne<User>(s => s.User)
                .WithMany(s => s.BugAndDefects)
                .HasForeignKey(s => s.ReportedBy).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
