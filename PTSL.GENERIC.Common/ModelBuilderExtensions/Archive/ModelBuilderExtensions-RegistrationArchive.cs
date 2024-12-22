using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.Entity.Archive;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureRegistrationArchive(this ModelBuilder builder)
        {
            builder.Entity<RegistrationArchive>(ac =>
            {
                ac.ToTable("RegistrationArchive", "Archive");

                ac.Property(a => a.ArchiveName)
                    .HasColumnType("varchar(500)")
                    .IsRequired(false);
                ac.Property(a => a.DocumentDescription)
                    .HasColumnType("varchar(500)")
                    .IsRequired(false);
            });

        }
    }
}
