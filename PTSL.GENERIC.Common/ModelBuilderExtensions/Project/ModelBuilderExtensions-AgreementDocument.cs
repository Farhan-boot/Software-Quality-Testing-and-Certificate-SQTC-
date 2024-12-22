using Microsoft.EntityFrameworkCore;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;

namespace PTSL.GENERIC.Common.ModelBuilderExtensions
{
    public static partial class EntityModelBuilderExtensions
    {
        public static void ConfigureAgreementDocument(this ModelBuilder builder)
        {
            builder.Entity<AgreementDocuments>(ac =>
            {
                ac.ToTable("AgreementDocuments", "GS");
            });
            builder.Entity<AgreementDocuments>().HasQueryFilter(q => !q.IsDeleted && q.IsActive);
            //builder.Entity<AgreementDocuments>().HasQueryFilter(q => q.IsDeleted == 0 && q.IsActive == 1);

        }

    }
}
