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
        public static void ConfigureProjectRequestLog(this ModelBuilder builder)
        {
            builder.Entity<ProjectRquestLog>(ac =>
            {
                ac.ToTable("ProjectRquestLog", "PR");
            });
            builder.Entity<ProjectRquestLog>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }
    }
}
