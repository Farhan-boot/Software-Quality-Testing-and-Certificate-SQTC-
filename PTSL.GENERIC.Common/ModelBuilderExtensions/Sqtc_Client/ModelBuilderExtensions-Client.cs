using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureClient(this ModelBuilder builder)
        {
            builder.Entity<Client>(ac =>
            {
                ac.ToTable("Client", "GS");
            });
            builder.Entity<Client>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<Client>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);
        }

    }
}
