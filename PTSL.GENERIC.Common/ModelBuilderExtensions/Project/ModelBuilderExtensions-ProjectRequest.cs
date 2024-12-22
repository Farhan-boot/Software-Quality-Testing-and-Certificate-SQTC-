using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureProjectRequest(this ModelBuilder builder)
        {
            builder.Entity<ProjectRequest>(ac =>
            {
                ac.ToTable("ProjectRequest", "PR");
            });
            builder.Entity<ProjectRequest>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
        }
    }
}
