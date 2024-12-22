using Microsoft.EntityFrameworkCore;

using PTSL.GENERIC.Common.ModelBuilderExtensions;

namespace PTSL.GENERIC.Common.Entity
{
    public class GENERICReadOnlyCtx : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public GENERICReadOnlyCtx(DbContextOptions<GENERICReadOnlyCtx> options)
            : base(options) => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        public GENERICReadOnlyCtx()
            : base() => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
#endif
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
            

            // GeneralSetup
            EntityModelBuilderExtensions.ConfigureCategory(modelBuilder);
            EntityModelBuilderExtensions.ConfigureCertification(modelBuilder);
            EntityModelBuilderExtensions.ConfigureDesignation(modelBuilder);
            EntityModelBuilderExtensions.ConfigureMeetingType(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTaskType(modelBuilder);
            EntityModelBuilderExtensions.ConfigureDocumentCategories(modelBuilder);
            EntityModelBuilderExtensions.ConfigureSkills(modelBuilder);


            // Archive
            EntityModelBuilderExtensions.ConfigureRegistrationArchive(modelBuilder);
            EntityModelBuilderExtensions.ConfigureRegistrationArchiveFile(modelBuilder);

            //Client
            EntityModelBuilderExtensions.ConfigureClient(modelBuilder);
            EntityModelBuilderExtensions.ConfigureClientLog(modelBuilder);
            EntityModelBuilderExtensions.ConfigureApprovalForRegisteredClientLog(modelBuilder);






            // PermissionSettings
            EntityModelBuilderExtensions.ConfigurePermissionHeaderSettings(modelBuilder);
            EntityModelBuilderExtensions.ConfigurePermissionRowSettings(modelBuilder);

            //Project Request
            EntityModelBuilderExtensions.ConfigureProjectRequest(modelBuilder);

            EntityModelBuilderExtensions.ConfigureProjectRequestLog(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTaskOfProject(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTaskLog(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTaskTimeTracking(modelBuilder);
            EntityModelBuilderExtensions.ConfigureApprovalForProjectLog(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTestScenario(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTestCategory(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTestCase(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTestStep(modelBuilder);
            EntityModelBuilderExtensions.ConfigureBugAndDefect(modelBuilder);
            EntityModelBuilderExtensions.ConfigureBugAndDefectFile(modelBuilder);
            EntityModelBuilderExtensions.ConfigureAgreementDocument(modelBuilder);
            EntityModelBuilderExtensions.ConfigureMeeting(modelBuilder);
            EntityModelBuilderExtensions.ConfigureAttendedUserMeeting(modelBuilder);
            EntityModelBuilderExtensions.ConfigureBugAndDefectLog(modelBuilder);

            ////Document
            EntityModelBuilderExtensions.ConfigureMeetingFiles(modelBuilder);
            EntityModelBuilderExtensions.ConfigureDocumentsByType(modelBuilder);
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
            EntityModelBuilderExtensions.ConfigurePaymentCalculationRow(modelBuilder);
            EntityModelBuilderExtensions.ConfigureHardwareTesting(modelBuilder);
            EntityModelBuilderExtensions.ConfigureTestScope(modelBuilder);
            EntityModelBuilderExtensions.ConfigurePaymentInformation(modelBuilder);
            EntityModelBuilderExtensions.ConfigureDepositSlipFile(modelBuilder);
            EntityModelBuilderExtensions.ConfigureReconciliation(modelBuilder);
            EntityModelBuilderExtensions.ConfigureFeedback(modelBuilder);
            EntityModelBuilderExtensions.ConfigureProjectStateLog(modelBuilder);
            EntityModelBuilderExtensions.ConfigureReviewComment(modelBuilder);
            
        }
    }
}
