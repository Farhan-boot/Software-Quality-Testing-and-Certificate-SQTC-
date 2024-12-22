using Microsoft.EntityFrameworkCore;
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
        public static void ConfigureTestScenario(this ModelBuilder builder)
        {
            builder.Entity<TestScenario>(ac =>
            {
                ac.ToTable("TestScenario", "PR");
            });
            builder.Entity<TestScenario>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }
    }
}
