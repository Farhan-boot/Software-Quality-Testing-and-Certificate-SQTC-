using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureAccessMapper(this ModelBuilder builder)
        {
            builder.Entity<AccessMapper>(ac =>
            {
                ac.ToTable("AccessMapper", "System");

                ac.Property(a => a.AccessList)
                    .HasColumnName("AccessList")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
                ac.Property(a => a.RoleStatus)
                    .HasColumnName("RoleStatus")
                    .HasColumnType("smallint")
                    .IsRequired();
                ac.Property(a => a.IsVisible)
                    .HasColumnName("IsVisible")
                    .HasColumnType("smallint")
                    .IsRequired();
            });
            builder.Entity<AccessMapper>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<AccessMapper>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
