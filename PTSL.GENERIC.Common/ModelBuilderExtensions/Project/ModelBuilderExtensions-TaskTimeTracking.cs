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
        public static void ConfigureTaskTimeTracking(this ModelBuilder builder)
        {
            builder.Entity<TaskTimeTracking>(ac =>
            {
                ac.ToTable("TaskTimeTracking", "PR");
            });
            builder.Entity<TaskTimeTracking>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }
    }
}
