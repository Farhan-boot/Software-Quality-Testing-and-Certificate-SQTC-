using AutoMapper;
using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;


using PTSL.GENERIC.Common.Entity.Project;

//using PTSL.GENERIC.Common.Entity.SocialForestry;
using PTSL.GENERIC.Common.Model;
using PTSL.GENERIC.Common.Model.EntityViewModels;
using PTSL.GENERIC.Common.Model.EntityViewModels.Archive;
using PTSL.GENERIC.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.GENERIC.Common.Model.EntityViewModels.PermissionSettings;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_Client;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ClientLog;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;
using PTSL.GENERIC.Common.Model.EntityViewModels.Sqtc_ApprovalForRegisteredClientLog;
using System.Security.Cryptography.Xml;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Model.EntityViewModels.Meetings;
using PTSL.GENERIC.Common.Model.EntityViewModels.Documents;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Model.EntityViewModels.SecurityTestings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity.HardwareTestings;
using PTSL.GENERIC.Common.Model.EntityViewModels.HardwareTestings;

namespace PTSL.GENERIC.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //general setup
            base.CreateMap<Category, CategoryVM>().ReverseMap();

            //User Manager
            base.CreateMap<User, UserVM>().ReverseMap(); //Source to Destination
            base.CreateMap<UserGroup, UserGroupVM>().ReverseMap();
         
            base.CreateMap<UserRole, UserRoleVM>().ReverseMap();
            base.CreateMap<Accesslist, AccesslistVM>().ReverseMap();
            base.CreateMap<AccessMapper, AccessMapperVM>().ReverseMap();
            base.CreateMap<Module, ModuleVM>().ReverseMap();
            base.CreateMap<PmsGroup, PmsGroupVM>().ReverseMap();
            //Marketing
            


             

            // Archive
            base.CreateMap<RegistrationArchive, RegistrationArchiveVM>().ReverseMap();
            base.CreateMap<RegistrationArchiveFile, RegistrationArchiveFileVM>().ReverseMap();

          

            //PermissionSettings
            base.CreateMap<PermissionHeaderSettings, PermissionHeaderSettingsVM>().ReverseMap();
            base.CreateMap<PermissionRowSettings, PermissionRowSettingsVM>().ReverseMap();

            //generalSetup

            base.CreateMap<Designation, DesignationVM>().ReverseMap();
            base.CreateMap<Certification, CertificationVM>().ReverseMap();
            base.CreateMap<DocumentCategories, DocumentCategoriesVM>().ReverseMap();
            base.CreateMap<TaskType, TaskTypeVM>().ReverseMap();
            base.CreateMap<MeetingType, MeetingTypeVM>().ReverseMap();
            base.CreateMap<Skill, SkillVM>().ReverseMap();

            //client
            base.CreateMap<Client, ClientVM>().ReverseMap();
            base.CreateMap<ClientLog, ClientLogVM>().ReverseMap();
            base.CreateMap<ApprovalForRegisteredClientLog, ApprovalForRegisteredClientLogVM>().ReverseMap();

            //Project
            base.CreateMap<ProjectRequest, ProjectRequestVM>().ReverseMap();
            base.CreateMap<ApprovalForProjectLog, ApprovalForProjectLogVM>().ReverseMap();
            base.CreateMap<ProjectRquestLog, ProjectRequestLogVM>().ReverseMap();
            base.CreateMap<TaskOfProject, TaskOfProjectVM>().ReverseMap();
            base.CreateMap<TaskLog, TaskLogVM>().ReverseMap();
            base.CreateMap<TaskTimeTracking, TaskTimeTrackingVM>().ReverseMap();
            base.CreateMap<TestScenario, TestScenarioVM>().ReverseMap();
            base.CreateMap<TestCase, TestCaseVM>().ReverseMap();
            base.CreateMap<TestCategory, TestCategoryVM>().ReverseMap();
            base.CreateMap<TestStep, TestStepVM>().ReverseMap();
            base.CreateMap<BugAndDefect, BugAndDefectVM>()
                .ForMember(x=>x.BugAndDefectFiles,y=>y.MapFrom(x=>x.BugAndDefectFiles)).ReverseMap();
            base.CreateMap<BugAndDefectFile, BugAndDefectFileVM>().ReverseMap();
            base.CreateMap<AgreementDocuments, AgreementDocumentsVM>().ReverseMap();
            base.CreateMap<Meeting, MeetingVM>()
                .ForMember(x=>x.AttendedUsers,y=>y.MapFrom(x=>x.AttendedUsers))
                .ForMember(x=>x.MeetingFiles,z=>z.MapFrom(x=>x.MeetingFiles)).ReverseMap();
            base.CreateMap<AttendedUserMeeting, AttendedUserMeetingVM>().ReverseMap();
            base.CreateMap<DocumentsByType, DocumentsByTypeVM>().ReverseMap();
            base.CreateMap<MeetingFiles, MeetingFilesVM>().ReverseMap();
            base.CreateMap<SecurityTesting, SecurityTestingVM>().ReverseMap();
            base.CreateMap<SecurityTestingFile, SecurityTestingFileVM>().ReverseMap();
            base.CreateMap<ProjectCertification, ProjectCertificationVM>().ReverseMap();
            base.CreateMap<DocumentAmendment, DocumentAmendmentVM>().ReverseMap();

            //ProjectPackageConfiguration
            base.CreateMap<ProjectModuleName, ProjectModuleNameVM>().ReverseMap();
            base.CreateMap<ProjectPackage, ProjectPackageVM>().ReverseMap();
            base.CreateMap<ProjectPricingSetup, ProjectPricingSetupVM>().ReverseMap();
            base.CreateMap<PaymentCalculationHeader, PaymentCalculationHeaderVM>().ReverseMap();
            base.CreateMap<PaymentCalculationRow, PaymentCalculationRowVM>().ReverseMap();
            base.CreateMap<PaymentInformation, PaymentInformationVM>().ReverseMap();
            base.CreateMap<DepositSlipFile, DepositSlipFileVM>().ReverseMap();
            base.CreateMap<Reconciliation, ReconciliationVM>().ReverseMap();
            base.CreateMap<Feedback, FeedbackVM>().ReverseMap();
            base.CreateMap<ReviewComment, ReviewCommentVM>().ReverseMap();

            base.CreateMap<BugAndDefectLog, BugAndDefectLogVM>().ReverseMap();
            //hardware
            base.CreateMap<TestScope, TestScopeVM>().ReverseMap();
            base.CreateMap<HardwareTesting, HardwareTestingVM>().ReverseMap();
            base.CreateMap<AllTypesOfDocument, AllTypesOfDocumentVM>().ReverseMap();
            base.CreateMap<ApprovalForAllDocument, ApprovalForAllDocumentVM>().ReverseMap();
            base.CreateMap<ProjectStateLog, ProjectStateLogVM>().ReverseMap();
            base.CreateMap<DefaultDocumentContent, DefaultDocumentContentVM>().ReverseMap();
        }


    }
}
