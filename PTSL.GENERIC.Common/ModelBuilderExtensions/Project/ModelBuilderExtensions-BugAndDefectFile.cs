using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureBugAndDefectFile(this ModelBuilder builder)
        {
            builder.Entity<BugAndDefectFile>(ac =>
            {
                ac.ToTable("BugAndDefectFile", "PR");
            });
            builder.Entity<BugAndDefectFile>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            builder.Entity<BugAndDefectFile>()
                .HasOne<BugAndDefect>(s => s.BugAndDefect)
                .WithMany(s => s.BugAndDefectFiles)
                .HasForeignKey(s => s.BugAndDefectId).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
