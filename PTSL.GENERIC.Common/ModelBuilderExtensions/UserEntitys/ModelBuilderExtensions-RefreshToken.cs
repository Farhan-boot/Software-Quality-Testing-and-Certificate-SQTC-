using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.Entity.UserEntitys;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureRefreshToken(this ModelBuilder builder)
        {
            builder.Entity<RefreshToken>(ac =>
            {
                ac.ToTable(nameof(RefreshToken), "System");

                ac.HasIndex(x => x.Token);
            });

            builder.Entity<RefreshToken>().HasQueryFilter(q => !q.IsRevoked);
        }
    }
}
