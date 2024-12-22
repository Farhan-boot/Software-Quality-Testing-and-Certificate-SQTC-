using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.PermissionSettings;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions;

public static partial class EntityModelBuilderExtensions
{
    public static void ConfigureAccesslist(this ModelBuilder builder)
    {
        builder.Entity<Accesslist>(ac =>
        {
            ac.ToTable("Accesslist", "System");

            ac.Property(a => a.ControllerName)
                .HasColumnName("ControllerName")
                .HasColumnType("varchar(500)")
                .IsRequired();
            ac.Property(a => a.ActionName)
                .HasColumnName("ActionName")
                .HasColumnType("varchar(500)")
                .IsRequired(false);
            ac.Property(a => a.Mask)
                .HasColumnName("Mask")
                .HasColumnType("varchar(500)")
                .IsRequired();
            ac.Property(a => a.AccessStatus)
                .HasColumnName("AccessStatus")
                .HasColumnType("smallint")
                .IsRequired();
            ac.Property(a => a.IsVisible)
                .HasColumnName("IsVisible")
                .HasColumnType("smallint")
                .IsRequired();
            ac.Property(a => a.IconClass)
                .HasColumnName("IconClass")
                .HasColumnType("varchar(500)")
                .IsRequired(false);
            ac.Property(a => a.ModuleId)
                .HasColumnName("ModuleId")
                .IsRequired(false);
            ac.Property(a => a.BaseModuleIndex)
                .HasColumnName("BaseModuleIndex")
                .HasColumnType("int")
                .IsRequired(false);
        });

        builder.Entity<Accesslist>()
            .HasOne(x => x.Module)
            .WithMany(x => x.AccessList)
            .HasForeignKey(x => x.ModuleId)
            .IsRequired(false);

        builder.Entity<Accesslist>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
    }
}
