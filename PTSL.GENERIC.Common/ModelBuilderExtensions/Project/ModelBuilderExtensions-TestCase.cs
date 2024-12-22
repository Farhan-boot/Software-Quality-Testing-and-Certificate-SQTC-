using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureTestCase(this ModelBuilder builder)
        {
            builder.Entity<TestCase>(ac =>
            {
                ac.ToTable("TestCase", "PR");
            });
            builder.Entity<TestCase>()
                .HasOne<User>(s => s.User)
                .WithMany(s => s.TestCases)
                .HasForeignKey(s => s.ExecutedByUserId).OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<TestCase>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }
    }
}
