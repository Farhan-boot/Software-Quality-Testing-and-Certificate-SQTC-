using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureModule(this ModelBuilder builder)
        {
            builder.Entity<Module>(ac =>
            {
                ac.ToTable("Module", "System");

                ac.Property(a => a.ModuleName)
                    .HasColumnName("ModuleName")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
                ac.Property(a => a.ModuleIcon)
                    .HasColumnName("ModuleIcon")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
                ac.Property(a => a.MenueOrder)
                    .HasColumnName("MenueOrder")
                    .HasColumnType("int")
                    .IsRequired();
            });
            builder.Entity<Module>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<Module>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
