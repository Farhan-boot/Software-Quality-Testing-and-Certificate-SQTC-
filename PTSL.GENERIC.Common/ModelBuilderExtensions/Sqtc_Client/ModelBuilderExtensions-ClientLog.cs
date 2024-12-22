using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureClientLog(this ModelBuilder builder)
        {
            builder.Entity<ClientLog>(ac =>
            {
                ac.ToTable("ClientLog", "GS");
            });
            builder.Entity<ClientLog>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<ClientLog>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
