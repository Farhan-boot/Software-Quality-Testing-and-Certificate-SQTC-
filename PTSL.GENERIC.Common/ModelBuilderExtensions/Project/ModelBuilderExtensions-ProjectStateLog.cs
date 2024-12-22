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
        public static void ConfigureProjectStateLog(this ModelBuilder builder)
        {
            builder.Entity<ProjectStateLog>(ac =>
            {
                ac.ToTable("ProjectStateLog", "PR");
            });
            builder.Entity<ProjectStateLog>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }
    }
}
