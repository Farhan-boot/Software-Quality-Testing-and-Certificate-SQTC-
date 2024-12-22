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
        public static void ConfigureBugAndDefectLog(this ModelBuilder builder)
        {
            builder.Entity<BugAndDefectLog>(ac =>
            {
                ac.ToTable("BugAndDefectLog", "PR");
            });
            builder.Entity<BugAndDefectLog>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }
    }
}
