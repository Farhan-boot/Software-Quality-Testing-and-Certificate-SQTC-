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
        public static void ConfigureTaskLog(this ModelBuilder builder)
        {
            builder.Entity<TaskLog>(ac =>
            {
                ac.ToTable("TaskLog", "PR");
            });
            builder.Entity<TaskLog>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }
    }
}
