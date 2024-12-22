using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.Entity.Archive;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureRegistrationArchiveFile(this ModelBuilder builder)
        {
            builder.Entity<RegistrationArchiveFile>(ac =>
            {
                ac.ToTable("RegistrationArchiveFile", "Archive");

                ac.Property(a => a.FileName)
                    .HasColumnType("varchar(500)")
                    .IsRequired(false);

                ac.Property(a => a.DocumentUrl)
                    .HasColumnType("varchar(500)")
                    .IsRequired(false);
            });


            builder.Entity<RegistrationArchiveFile>()
              .HasOne(x => x.RegistrationArchive)
              .WithMany(x => x.RegistrationArchiveFiles)
              .HasForeignKey(x => x.RegistrationArchiveId)
              .IsRequired(false);
        }
    }
}
