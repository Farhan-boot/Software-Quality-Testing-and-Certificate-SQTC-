using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.GeneralSetup;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureSkills(this ModelBuilder builder)
        {
            builder.Entity<Skill>(ac =>
            {
                ac.ToTable("Skill", "GS");

                ac.Property(a => a.SkillName)
                    .HasColumnName("SkillName")
                    .HasColumnType("varchar(500)")
                    .IsRequired();
            });
            builder.Entity<Skill>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<Category>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }
    }
}
