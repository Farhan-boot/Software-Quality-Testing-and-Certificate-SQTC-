using PTSL.GENERIC.Business.Businesses.Implementation;
using PTSL.GENERIC.Business.Businesses.Implementation.Archive;
using PTSL.GENERIC.Business.Businesses.Implementation.Documents;
using PTSL.GENERIC.Business.Businesses.Implementation.GeneralSetup;
using PTSL.GENERIC.Business.Businesses.Implementation.Meetings;
using PTSL.GENERIC.Business.Businesses.Implementation.PermissionSettings;
using PTSL.GENERIC.Business.Businesses.Implementation.Project;
using PTSL.GENERIC.Business.Businesses.Implementation.SecurityTestings;
using PTSL.GENERIC.Business.Businesses.Implementation.ProjectPackageConfiguration;


//using PTSL.GENERIC.Business.Businesses.Implementation.SocialForestry;
//using PTSL.GENERIC.Business.Businesses.Implementation.SocialForestryPatrollingSchedule;
using PTSL.GENERIC.Business.Businesses.Implementation.SystemUser;
using PTSL.GENERIC.Business.Businesses.Interface;
using PTSL.GENERIC.Business.Businesses.Interface.Archive;
using PTSL.GENERIC.Business.Businesses.Interface.Documents;
using PTSL.GENERIC.Business.Businesses.Interface.GeneralSetup;
using PTSL.GENERIC.Business.Businesses.Interface.Meetings;
using PTSL.GENERIC.Business.Businesses.Interface.PermissionSettings;
using PTSL.GENERIC.Business.Businesses.Interface.Project;
using PTSL.GENERIC.Business.Businesses.Interface.SecurityTestings;
using PTSL.GENERIC.Business.Businesses.Interface.ProjectPackageConfiguration;


//using PTSL.GENERIC.Business.Businesses.Interface.SocialForestry;
//using PTSL.GENERIC.Business.Businesses.Interface.SocialForestryPatrollingSchedule;
using PTSL.GENERIC.Business.Businesses.Interface.SystemUser;
using PTSL.GENERIC.Business.TokenHelper;
using PTSL.GENERIC.DAL.Repositories.Implementation;
using PTSL.GENERIC.DAL.Repositories.Implementation.Archive;
using PTSL.GENERIC.DAL.Repositories.Implementation.Documents;
using PTSL.GENERIC.DAL.Repositories.Implementation.GeneralSetup;
using PTSL.GENERIC.DAL.Repositories.Implementation.PermissionSettings;
using PTSL.GENERIC.DAL.Repositories.Implementation.Project;
using PTSL.GENERIC.DAL.Repositories.Implementation.SecurityTestings;

using PTSL.GENERIC.DAL.Repositories.Implementation.ProjectPackageConfiguration;

//using PTSL.GENERIC.DAL.Repositories.Implementation.SocialForestry;
//using PTSL.GENERIC.DAL.Repositories.Implementation.SocialForestryPatrollingSchedule;
using PTSL.GENERIC.DAL.Repositories.Implementation.SystemUser;
using PTSL.GENERIC.DAL.Repositories.Interface;
using PTSL.GENERIC.DAL.Repositories.Interface.Archive;
using PTSL.GENERIC.DAL.Repositories.Interface.AttendedUserMeetings;
using PTSL.GENERIC.DAL.Repositories.Interface.Documents;
using PTSL.GENERIC.DAL.Repositories.Interface.GeneralSetup;
using PTSL.GENERIC.DAL.Repositories.Interface.MeetingFiless;
using PTSL.GENERIC.DAL.Repositories.Interface.Meetings;
using PTSL.GENERIC.DAL.Repositories.Interface.PermissionSettings;
using PTSL.GENERIC.DAL.Repositories.Interface.Project;
using PTSL.GENERIC.DAL.Repositories.Interface.SecurityTestingFiles;
using PTSL.GENERIC.DAL.Repositories.Interface.SecurityTestings;
using PTSL.GENERIC.DAL.Repositories.Interface.ProjectPackageConfiguration;


//using PTSL.GENERIC.DAL.Repositories.Interface.SocialForestry;
//using PTSL.GENERIC.DAL.Repositories.Interface.SocialForestryPatrollingSchedule;
using PTSL.GENERIC.DAL.Repositories.Interface.SystemUser;
using PTSL.GENERIC.DAL.UnitOfWork;
using PTSL.GENERIC.Service.Services;
using PTSL.GENERIC.Service.Services.Archive;
using PTSL.GENERIC.Service.Services.Implementation;
using PTSL.GENERIC.Service.Services.Implementation.Documents;
using PTSL.GENERIC.Service.Services.Implementation.GeneralSetup;
using PTSL.GENERIC.Service.Services.Implementation.Project;
using PTSL.GENERIC.Service.Services.Implementation.SecurityTestings;
using PTSL.GENERIC.Service.Services.Implementation.SystemUser;
using PTSL.GENERIC.Service.Services.Interface;
using PTSL.GENERIC.Service.Services.Interface.Documents;
using PTSL.GENERIC.Service.Services.Interface.GeneralSetup;
using PTSL.GENERIC.Service.Services.Interface.Meetings;
using PTSL.GENERIC.Service.Services.Interface.Project;
using PTSL.GENERIC.Service.Services.Interface.SystemUser;
using PTSL.GENERIC.Service.Services.PermissionSettings;
using PTSL.GENERIC.Service.Services.ProjectPackageConfiguration;
using PTSL.GENERIC.Business.Businesses.Interface.HardwareTestings;
using PTSL.GENERIC.Business.Businesses.Implementation.HardwareTestings;
using PTSL.GENERIC.Service.Services.Interface.HardwareTestings;
using PTSL.GENERIC.DAL.Repositories.Interface.HardwareTestings;
using PTSL.GENERIC.DAL.Repositories.Implementation.HardwareTestings;
using PTSL.GENERIC.Service.Services.Implementation.HardwareTestings;
using PTSL.GENERIC.Common.Model.EntityViewModels.SystemUser;
//using PTSL.GENERIC.Service.Services.SocialForestry;
//using PTSL.GENERIC.Service.Services.SocialForestryPatrollingSchedule;

namespace PTSL.GENERIC.Api.Helpers
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddDependecyResolver(this IServiceCollection services)
        {
            //UOW
            services.AddScoped<IGENERICUnitOfWork, GENERICUnitOfWork>();

            // Repository
            AddScopedForRepository(services);

            // Services
            AddScopedForService(services);

            // Business
            AddScopedForBusiness(services);

            services.AddSingleton<FileHelper>();
            services.AddSingleton<EmailService>();
            services.AddSingleton<IGenerateJSONWebToken, GenerateJSONWebToken>();
            

            return services;
        }

        private static void AddScopedForBusiness(IServiceCollection services)
        {
            

            services.AddScoped<IRefreshTokenBusiness, RefreshTokenBusiness>();

            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IUserGroupBusiness, UserGroupBusiness>();
            services.AddScoped<ICategoryBusiness, CategoryBusiness>();
            
            services.AddScoped<IUserRoleBusiness, UserRoleBusiness>();
            services.AddScoped<IAccesslistBusiness, AccesslistBusiness>();
            services.AddScoped<IAccessMapperBusiness, AccessMapperBusiness>();
            services.AddScoped<IModuleBusiness, ModuleBusiness>();
            services.AddScoped<IPmsGroupBusiness, PmsGroupBusiness>();

            services.AddScoped<IDesignationBusiness, DesignationBusiness>();
            services.AddScoped<ICertificationBusiness, CertificationBusiness>();
            services.AddScoped<IDocumentCategoriesBusiness, DocumentCategoriesBusiness>();
            services.AddScoped<ITaskTypeBusiness, TaskTypeBusiness>();
            services.AddScoped<IMeetingTypeBusiness, MeetingTypeBusiness>();
            services.AddScoped<ISkillBusiness, SkillBusiness>();

            services.AddScoped<IMeetingBusiness, MeetingBusiness>();
            services.AddScoped<IAttendedUserMeetingBusiness, AttendedUserMeetingBusiness>();




            // Archive
            services.AddScoped<IRegistrationArchiveBusiness, RegistrationArchiveBusiness>();
            services.AddScoped<IRegistrationArchiveFileBusiness, RegistrationArchiveFileBusiness>();


            // PermissionSettings
            services.AddScoped<IPermissionHeaderSettingsBusiness, PermissionHeaderSettingsBusiness>();
            services.AddScoped<IPermissionRowSettingsBusiness, PermissionRowSettingsBusiness>();
            //client
            services.AddScoped<IClientBusiness, ClientBusiness>();
            services.AddScoped<IClientLogBusiness, ClientLogBusiness>();
            services.AddScoped<IApprovalForRegisteredClientLogBusiness, ApprovalForRegisteredClientLogBusiness>();
            //Projet
            services.AddScoped<IProjectRequestBusiness, ProjectRequestBusiness>();
            services.AddScoped<IProjectRequestLogBusiness, ProjectRequestLogBusiness>();
            services.AddScoped<ITaskBusiness, TaskBusiness>();
            services.AddScoped<ITaskLogBusiness, TaskLogBusiness>();
            services.AddScoped<ITaskTimeTrackingBusiness, TaskTimeTrackingBusiness>();
            services.AddScoped<IApprovalForProjectLogBusiness, ApprovalForProjectLogBusiness>();
            services.AddScoped<ITestScenarioBusiness, TestScenarioBusiness>();
            services.AddScoped<ITestCaseBusiness, TestCaseBusiness>();
            services.AddScoped<ITestStepBusiness, TestStepBusiness>();
            services.AddScoped<ITestCategoryBusiness, TestCategoryBusiness>();
            services.AddScoped<IBugAndDefectBusiness, BugAndDefectBusiness>();
            services.AddScoped<IBugAndDefectFileBusiness, BugAndDefectFileBusiness>();
            services.AddScoped<IAgreementDocumentBusiness, AgreementDocumentBusiness>();
            services.AddScoped<IBugAndDefectLogBusiness, BugAndDefectLogBusiness>();

            //Document
            services.AddScoped<IDocumentsBusiness, DocumentBusiness>();
            services.AddScoped<IMeetingFilesBusiness, MeetingFilesBusiness>();
            services.AddScoped<ISecurityTestingBusiness, SecurityTestingBusiness>();
            services.AddScoped<ISecurityTestingFileBusiness, SecurityTestingFileBusiness>();
            services.AddScoped<IAllTypesOfDocumentBusiness, AllTypesOfDocumentBusiness>();
            services.AddScoped<IApprovalForAllDocumentBusiness, ApprovalForAllDocumentBusiness>();
            services.AddScoped<IDocumentAmendmentBusiness, DocumentAmendmentBusiness>();
            services.AddScoped<IProjectCertificationBusiness, ProjectCertificationBusiness>();
            services.AddScoped<IDefaultDocContentBusiness, DefaultDocContentBusiness>();

            //ProjectPackageConfiguration
            services.AddScoped<IProjectModuleNameBusiness, ProjectModuleNameBusiness>();
            services.AddScoped<IProjectPackageBusiness, ProjectPackageBusiness>();
            services.AddScoped<IProjectPricingSetupBusiness, ProjectPricingSetupBusiness>();
            services.AddScoped<IPaymentCalculationHeaderBusiness, PaymentCalculationHeaderBusiness>();
            services.AddScoped<IPaymentCalculationRowBusiness, PaymentCalculationRowBusiness>();
            services.AddScoped<IPaymentInformationBusiness, PaymentInformationBusiness>();
            services.AddScoped<IDepositSlipFileBusiness, DepositSlipFileBusiness>();
            services.AddScoped<IReconciliationBusiness, ReconciliationBusiness>();
            services.AddScoped<IFeedbackBusiness, FeedbackBusiness>();
            services.AddScoped<IReviewCommentBusiness, ReviewCommentBusiness>();

            //Hardware
            services.AddScoped<ITestScopeBusiness, TestScopeBusiness>();
            services.AddScoped<IHardwareTestingBusiness, HardwareTestingBusiness>();
            services.AddScoped<IProjectStateLogBusiness, ProjectStateLogBusiness>();


        }

        private static void AddScopedForService(IServiceCollection services)
        {
            

            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserGroupService, UserGroupService>();
            services.AddScoped<ICategoryService, CategoryService>();
            
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IAccesslistService, AccesslistService>();
            services.AddScoped<IAccessMapperService, AccessMapperService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IPmsGroupService, PmsGroupService>();
            

            

            // Archive
            services.AddScoped<IRegistrationArchiveService, RegistrationArchiveService>();
            services.AddScoped<IRegistrationArchiveFileService, RegistrationArchiveFileService>();

           

            // PermissionSettings
            services.AddScoped<IPermissionHeaderSettingsService, PermissionHeaderSettingsService>();
            services.AddScoped<IPermissionRowSettingsService, PermissionRowSettingsService>();

            //general Set up
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<ICertificationService, CertificationService>();
            services.AddScoped<IDocumentCategoriesService, DocumentCategoriesService>();
            services.AddScoped<ITaskTypeService, TaskTypeService>();
            services.AddScoped<IMeetingTypeService, MeetingTypeService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<IAttendedUserMeetingService, AttendedUserMeetingService>();

            //Client
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientLogService, ClientLogService>();
            services.AddScoped<IApprovalForRegisteredClientLogService, ApprovalForRegisteredClientLogService>();

            //Project
            services.AddScoped<IProjectRequestService, ProjectRequestService>();
            services.AddScoped<IProjectRequestLogService, ProjectRequestLogService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskLogService, TaskLogService>();
            services.AddScoped<ITaskTimeTrackingService, TaskTimeTrackingService>();
            services.AddScoped<IApprovalForProjectLogService, ApprovalForProjectLogService>();
            services.AddScoped<ITestScenarioService, TestScenarioService>();
            services.AddScoped<ITestCaseService, TestCaseService>();
            services.AddScoped<ITestStepService, TestStepService>();
            services.AddScoped<ITestCategoryService, TestCategoryService>();
            services.AddScoped<IBugAndDefectService, BugAndDefectService>();
            services.AddScoped<IBugAndDefectFileService,BugAndDefectFileService>();
            services.AddScoped<IAgreementDocumentService,AgreementDocumentService>();
            services.AddScoped<IBugAndDefectLogService, BugAndDefectLogService>();

            //Documents
            services.AddScoped<IDocumentsService, DocumentsService>();
            services.AddScoped<IMeetingFilesService, MeetingFilesService>();
            services.AddScoped<ISecurityTestingFileService, SecurityTestingFileService>();
            services.AddScoped<ISecurityTestingService, SecurityTestingService>();
            services.AddScoped<IAllTypesOfDocumentService, AllTypesOfDocumentService>();
            services.AddScoped<IApprovalForAllDocumentService, ApprovalForAllDocumentService>();
            services.AddScoped<IDocumentAmendmentService, DocumentAmendmentService>();
            services.AddScoped<IProjectCertificationService, ProjectCertificationService>();
            services.AddScoped<IDefaultDocContentService, DefaultDocService>();

            //ProjectPackageConfiguration
            services.AddScoped<IProjectModuleNameService, ProjectModuleNameService>();
            services.AddScoped<IProjectPackageService, ProjectPackageService>();
            services.AddScoped<IProjectPricingSetupService, ProjectPricingSetupService>();
            services.AddScoped<IPaymentCalculationHeaderService, PaymentCalculationHeaderService>();
            services.AddScoped<IPaymentCalculationRowService, PaymentCalculationRowService>();
            services.AddScoped<IPaymentInformationService, PaymentInformationService>();
            services.AddScoped<IDepositSlipFileService, DepositSlipFileService>();
            services.AddScoped<IReconciliationService, ReconciliationService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IReviewCommentService, ReviewCommentService>();

            //hardware
            services.AddScoped<ITestScopeService, TestScopeService>();
            services.AddScoped<IHardwareTestingService, HardwareTestingService>();

            services.AddScoped<IProjectStateLogService, ProjectStateLogService>();

            //Email


        }

        private static void AddScopedForRepository(IServiceCollection services)
        {
            

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            

            services.AddScoped<IUserGroupRepository, UserGroupRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserGroupRepository, UserGroupRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IAccesslistRepository, AccesslistRepository>();
            services.AddScoped<IAccessMapperRepository, AccessMapperRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IPmsGroupRepository, PmsGroupRepository>();
            

            

            

           
            // Archive
            services.AddScoped<IRegistrationArchiveRepository, RegistrationArchiveRepository>();
            services.AddScoped<IRegistrationArchiveFileRepository, RegistrationArchiveFileRepository>();
            // CipManagement
           
            // PermissionSettings
            services.AddScoped<IPermissionHeaderSettingsRepository, PermissionHeaderSettingsRepository>();
            services.AddScoped<IPermissionRowSettingsRepository, PermissionRowSettingsRepository>();

            //General Setup
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<ICertificationRepository, CertificationRepository>();
            services.AddScoped<IDocumentCategoriesRepository, DocumentCategoriesRepository>();
            services.AddScoped<ITaskTypeRepository, TaskTypeRepository>();
            services.AddScoped<IMeetingTypeRepository, MeetingTypeRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IAttendedUserMeetingRepository, AttendedUserMeetingRepository>();

            //Project
            services.AddScoped<IProjectRequestRepository, ProjectRequestRepository>();
            services.AddScoped<IProjectRequestLogRepository, ProjectRequestLogRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskLogRepository, TaskLogRepository>();
            services.AddScoped<ITaskTimeTrackingRepository, TaskTimeTrackingRepository>();
            services.AddScoped<IApprovalForProjectLogRepository, ApprovalForProjectLogRepository>();
            services.AddScoped<ITestScenarioRepository, TestScenarioRepository>();
            services.AddScoped<ITestCaseRepository, TestCaseRepository>();
            services.AddScoped<ITestStepRepository, TestStepRepository>();
            services.AddScoped<ITestCategoryRepository, TestCategoryRepository>();
            services.AddScoped<IBugAndDefectLogRepository, BugAndDefectLogRepository>();
            services.AddScoped<IProjectCertificationRepository, ProjectCertificationRepository>();

            //client
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientLogRepository, ClientLogRepository>();
            services.AddScoped<IApprovalForRegisteredClientLogRepository, ApprovalForRegisteredClientLogRepository>();
            services.AddScoped<IBugAndDefectRepository, BugAndDefectRepository>();
            services.AddScoped<IBugAndDefectFileRepository, BugAndDefectFileRepository>();
            services.AddScoped<IAgreementDocumentRepository, AgreementDocumentRepository>();

            //Documents
            services.AddScoped<IDocumentsRepository, DocumentsRepository>();
            services.AddScoped<IMeetingFilesRepository, MeetingFilesRepository>();
            services.AddScoped<ISecurityTestingFileRepository, SecurityTestingFileRepository>();
            services.AddScoped<ISecurityTestingRepository, SecurityTestingRepository>();
            services.AddScoped<IAllTypesOfDocumentRepository, AllTypesOfDocumentRepository>();
            services.AddScoped<IApprovalForAllDocumentRepository, ApprovalForAllDocumentRepository>();
            services.AddScoped<IDocAmendmentRepository, DocAmendmentRepository>();
            services.AddScoped<IDefaultDocContentRepository, DefaultDocContentRepository>();

            //ProjectPackageConfiguration
            services.AddScoped<IProjectModuleNameRepository, ProjectModuleNameRepository>();
            services.AddScoped<IProjectPackageRepository, ProjectPackageRepository>();
            services.AddScoped<IProjectPricingSetupRepository, ProjectPricingSetupRepository>();
            services.AddScoped<IPaymentCalculationHeaderRepository, PaymentCalculationHeaderRepository>();
            services.AddScoped<IPaymentCalculationRowRepository, PaymentCalculationRowRepository>();
            services.AddScoped<IPaymentInformationRepository, PaymentInformationRepository>();
            services.AddScoped<IDepositSlipFileRepository, DepositSlipFileRepository>();
            services.AddScoped<IReconciliationRepository, ReconciliationRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IReviewCommentRepository, ReviewCommentRepository>();

            //Hardware
            services.AddScoped<ITestScopeRepository, TestScopeRepository>();
            services.AddScoped<IHardwareTestingRepository, HardwareTestingRepository>();
            services.AddScoped<IProjectStateLogRepository, ProjectStateLogRepository>();

        }
    }
}
