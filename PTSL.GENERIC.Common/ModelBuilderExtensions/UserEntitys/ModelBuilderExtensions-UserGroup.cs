using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureUserGroup(this ModelBuilder builder)
        {
            builder.Entity<UserGroup>(ac =>
            {
                ac.ToTable("UserGroup", "System");

                ac.Property(a => a.GroupName)
                    .HasColumnName("GroupName")
                    .HasColumnType("varchar(40)")
                    .IsRequired();

                ac.Property(a => a.ModuleActionNames)
                    .HasColumnName("ModuleActionNames")
                    .HasColumnType("varchar(500)")
                    .IsRequired();

                ac.Property(a => a.IsUsed)
                    .HasColumnName("IsUsed")
                    .HasColumnType("smallint");
                ac.Property(a => a.IsDefault)
                    .HasColumnName("IsDefault")
                    .HasColumnType("smallint");

                ac.Property(a => a.IsVisible)
                    .HasColumnName("IsVisible")
                    .HasColumnType("smallint");
            });
            builder.Entity<UserGroup>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<UserGroup>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }
    }
}
