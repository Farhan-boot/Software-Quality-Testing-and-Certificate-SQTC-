using EntityFrameworkCore.Triggers;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using PTSL.GENERIC.Common.ModelBuilderExtensions;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace PTSL.GENERIC.Common.Entity
{
    public class GENERICWriteOnlyCtx : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public GENERICWriteOnlyCtx(DbContextOptions<GENERICWriteOnlyCtx> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public GENERICWriteOnlyCtx()
            : base()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
#endif
        }

        public override EntityEntry Attach(object entity)
        {
            return base.Entry(entity).State == EntityState.Detached
                ? base.Attach(entity)
                : base.Entry(entity);
        }

        public override EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
        {
            return base.Entry(entity).State == EntityState.Detached
                ? base.Attach(entity)
                : base.Entry(entity);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesWithTriggersAsync(
                    base.SaveChangesAsync,
                    acceptAllChangesOnSuccess: true,
                    cancellationToken: cancellationToken);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            EntityModelBuilderExtensions.ConfigureUser(modelBuilder);
            EntityModelBuilderExtensions.ConfigureRefreshToken(modelBuilder);
            EntityModelBuilderExtensions.ConfigureUserGroup(modelBuilder);
            EntityModelBuilderExtensions.ConfigureUserRole(modelBuilder);
            EntityModelBuilderExtensions.ConfigureUserRolePermissionMap(modelBuilder);
            EntityModelBuilderExtensions.ConfigureAccesslist(modelBuilder);
            EntityModelBuilderExtensions.ConfigureAccessMapper(modelBuilder);
            EntityModelBuilderExtensions.ConfigureModule(modelBuilder);
            EntityModelBuilderExtensions.ConfigurePmsGroup(modelBuilder);


            //General Setup
            EntityModelBuilderExtensions.ConfigureCertification(modelBuilder);
            EntityModelBuilderExtensions.ConfigureCategory(modelBuilder);
            EntityModelBuilderExtensions.ConfigureDesignation(modelBuilder);
            EntityModelBuilderExtensions.ConfigureMeetingType(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTaskType(modelBuilder);
            EntityModelBuilderExtensions.ConfigureDocumentCategories(modelBuilder);
            EntityModelBuilderExtensions.ConfigureSkills(modelBuilder);




            // Archive
            EntityModelBuilderExtensions.ConfigureRegistrationArchive(modelBuilder);
            EntityModelBuilderExtensions.ConfigureRegistrationArchiveFile(modelBuilder);

            

            // PermissionSettings
            EntityModelBuilderExtensions.ConfigurePermissionHeaderSettings(modelBuilder);
            EntityModelBuilderExtensions.ConfigurePermissionRowSettings(modelBuilder);

            //Client
            EntityModelBuilderExtensions.ConfigureClient(modelBuilder);

            EntityModelBuilderExtensions.ConfigureClientLog(modelBuilder);
            EntityModelBuilderExtensions.ConfigureApprovalForRegisteredClientLog(modelBuilder);

            //Project Request
            EntityModelBuilderExtensions.ConfigureProjectRequest(modelBuilder);

            EntityModelBuilderExtensions.ConfigureProjectRequestLog(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTaskOfProject(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTaskLog(modelBuilder);
            EntityModelBuilderExtensions.ConfigureApprovalForProjectLog(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTaskTimeTracking(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTestScenario(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTestCase(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTestStep(modelBuilder);
            EntityModelBuilderExtensions.ConfigureBugAndDefect(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTestCategory(modelBuilder);
            EntityModelBuilderExtensions.ConfigureBugAndDefectFile(modelBuilder);
            EntityModelBuilderExtensions.ConfigureAgreementDocument(modelBuilder);
            EntityModelBuilderExtensions.ConfigureMeeting(modelBuilder);
            EntityModelBuilderExtensions.ConfigureAttendedUserMeeting(modelBuilder);
            EntityModelBuilderExtensions.ConfigureBugAndDefectLog(modelBuilder);

            ////Document
            EntityModelBuilderExtensions.ConfigureDocumentsByType(modelBuilder);
            EntityModelBuilderExtensions.ConfigureMeetingFiles(modelBuilder);
            EntityModelBuilderExtensions.ConfigureSecurityTesting(modelBuilder);
            EntityModelBuilderExtensions.ConfigureSecurityTestingFile(modelBuilder);
            EntityModelBuilderExtensions.ConfigureAllTypesOfDocument(modelBuilder);
            EntityModelBuilderExtensions.ConfigureApprovalForAllDocument(modelBuilder);
            EntityModelBuilderExtensions.ConfigureProjectCertification(modelBuilder);
            EntityModelBuilderExtensions.ConfigureDocumentAmendment(modelBuilder);
            EntityModelBuilderExtensions.ConfigureDefaultDocumentContent(modelBuilder);

            //ProjectPackageConfiguration
            EntityModelBuilderExtensions.ConfigureProjectModuleName(modelBuilder);
            EntityModelBuilderExtensions.ConfigureProjectPackage(modelBuilder);
            EntityModelBuilderExtensions.ConfigureProjectPricingSetup(modelBuilder);
            EntityModelBuilderExtensions.ConfigurePaymentCalculationHeader(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTestScope(modelBuilder);
            EntityModelBuilderExtensions.ConfigurePaymentCalculationRow(modelBuilder);
            EntityModelBuilderExtensions.ConfigureHardwareTesting(modelBuilder);
            EntityModelBuilderExtensions.ConfigurePaymentInformation(modelBuilder);
            EntityModelBuilderExtensions.ConfigureDepositSlipFile(modelBuilder);
            EntityModelBuilderExtensions.ConfigureReconciliation(modelBuilder);
            EntityModelBuilderExtensions.ConfigureFeedback(modelBuilder);
            EntityModelBuilderExtensions.ConfigureReviewComment(modelBuilder);
            EntityModelBuilderExtensions.ConfigureProjectStateLog(modelBuilder);

        }
    }
}
