using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigurePmsGroup(this ModelBuilder builder)
        {
            builder.Entity<PmsGroup>(ac =>
            {
                ac.ToTable("PmsGroup", "System");

                ac.Property(a => a.GroupName)
                    .HasColumnName("GroupName")
                    .HasColumnType("varchar(40)")
                    .IsRequired();

                ac.Property(a => a.GroupDescription)
                    .HasColumnName("GroupDescription")
                    .HasColumnType("varchar(250)")
                    .IsRequired();

                ac.Property(a => a.GroupStatus)
                    .HasColumnName("GroupStatus")
                    .HasColumnType("smallint");

                ac.Property(a => a.IsVisible)
                    .HasColumnName("IsVisible")
                    .HasColumnType("smallint");
            });
            builder.Entity<PmsGroup>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<PmsGroup>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }
    }
}
