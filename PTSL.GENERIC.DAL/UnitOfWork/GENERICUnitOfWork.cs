using Microsoft.EntityFrameworkCore.Storage;

using PTSL.GENERIC.Common.Entity;
using PTSL.GENERIC.Common.Entity.Archive;
using PTSL.GENERIC.Common.Entity.CommonEntity;
using PTSL.GENERIC.Common.Entity.Documents;
using PTSL.GENERIC.Common.Entity.GeneralSetup;
using PTSL.GENERIC.Common.Entity.Meetings;
using PTSL.GENERIC.Common.Entity.PermissionSettings;
using PTSL.GENERIC.Common.Entity.Project;
using PTSL.GENERIC.Common.Entity.SecurityTestings;
using PTSL.GENERIC.Common.Entity.ProjectPackageConfiguration;
using PTSL.GENERIC.Common.Entity.Sqtc_Client;
using PTSL.GENERIC.Common.Entity.Sqtc_ClientLog;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
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
using System;
using System.Linq;
using System.Threading.Tasks;
using PTSL.GENERIC.DAL.Repositories.Interface.HardwareTestings;
using PTSL.GENERIC.Common.Entity.HardwareTestings;

namespace PTSL.GENERIC.DAL.UnitOfWork
{
    public class GENERICUnitOfWork : IGENERICUnitOfWork
    {
        private GENERICWriteOnlyCtx WriteOnlyCtx { get; }
        private GENERICReadOnlyCtx ReadOnlyCtx { get; }

        public IUserRepository users { get; set; }
        public IUserGroupRepository usergroups { get; set; }

        public IUserRoleRepository UserRoles { get; set; }
        public IAccesslistRepository Accesslist { get; set; }
        public IAccessMapperRepository AccessMapper { get; set; }
        public IModuleRepository Module { get; set; }
        public IPmsGroupRepository PmsGroup { get; set; }





        public IRegistrationArchiveRepository RegistrationArchiveRepository { get; set; }
        public IRegistrationArchiveFileRepository RegistrationArchiveFileRepository { get; set; }



        //PermissionSettings
        public IPermissionHeaderSettingsRepository PermissionHeaderSettingsRepository { get; set; }
        public IPermissionRowSettingsRepository PermissionRowSettingsRepository { get; set; }
        //General Setup

        public ICategoryRepository Categoriess { get; set; }
        public ICertificationRepository CertificationRepository { get; set; }
        public IDocumentCategoriesRepository DocumentCategoriesRepository { get; set; }
        public IDesignationRepository DesignationRepository { get; set; }
        public IMeetingTypeRepository MeetingTypeRepository { get; set; }
        public ITaskTypeRepository TaskTypeRepository { get; set; }
        public ISkillRepository SkillRepository { get; set; }
        //Client
        public IClientRepository ClientRepository { get; set; }
        public IClientLogRepository ClientLogRepository { get; set; }
        public IApprovalForRegisteredClientLogRepository ApprovalForRegisteredClientLogRepository { get; set; }

        //Project
        public IProjectRequestRepository ProjectRequestRepository { get; set; }
        public IProjectRequestLogRepository ProjectRequestLogRepository { get; set; }
        public ITaskRepository TaskRepository { get; set; }
        public ITaskLogRepository TaskLogRepository { get; set; }
        public ITaskTimeTrackingRepository TaskTimeTrackingRepository { get; set; }
        public IApprovalForProjectLogRepository ApprovalForProjectLogRepository { get; set; }
        public ITestScenarioRepository TestScenarioRepository { get; set; }
        public ITestCaseRepository TestCaseRepository { get; set; }
        public ITestStepRepository TestStepRepository { get; set; }
        public ITestCategoryRepository TestCategoryRepository { get; set; }
        public IBugAndDefectRepository BugAndDefectRepository { get; set; }
        public IBugAndDefectFileRepository BugAndDefectFileRepository { get; set; }
        public IAgreementDocumentRepository AgreementDocumentRepository { get; set; }
        public IMeetingRepository MeetingRepository { get; set; }
        public IAttendedUserMeetingRepository AttendedUserMeetingRepository { get; set; }
        public IDocumentsRepository DocumentsRepository { get; set; }
        public IMeetingFilesRepository MeetingFilesRepository { get; set; }
        public ISecurityTestingRepository SecurityTestingRepository { get; set; }
        public ISecurityTestingFileRepository SecurityTestingFileRepository { get; set; }

        public IBugAndDefectLogRepository BugAndDefectLogRepository { get; set; }
        //ProjectPackageConfiguration
        public IProjectModuleNameRepository ProjectModuleNameRepository { get; set; }
        public IProjectPackageRepository ProjectPackageRepository { get; set; }
        public IProjectPricingSetupRepository ProjectPricingSetupRepository { get; set; }
        public ITestScopeRepository TestScopeRepository { get; set; }
        public IHardwareTestingRepository HardwareTestingRepository { get; set; }

        public IPaymentCalculationHeaderRepository PaymentCalculationHeaderRepository { get; set; }
        public IPaymentCalculationRowRepository PaymentCalculationRowRepository { get; set; }
        public IAllTypesOfDocumentRepository AllTypesOfDocumentRepository { get; set; }


        public IPaymentInformationRepository PaymentInformationRepository { get; set; }
        public IDepositSlipFileRepository DepositSlipFileRepository { get; set; }
        public IApprovalForAllDocumentRepository ApprovalForAllDocumentRepository { get; set; }

        public IReconciliationRepository ReconciliationRepository { get; set; }
        public IFeedbackRepository FeedbackRepository { get; set; }
        public IProjectStateLogRepository ProjectStateLogRepository { get; set; }
        public IDocAmendmentRepository DocAmendmentRepository { get; set; }
        public IProjectCertificationRepository ProjectCertificationRepository { get; set; }
        public IReviewCommentRepository ReviewCommentRepository { get; set; }
        public IDefaultDocContentRepository DefaultDocContentRepository { get; set; }

        public GENERICUnitOfWork(
            GENERICWriteOnlyCtx ecommarceWriteOnlyCtx,
            GENERICReadOnlyCtx ecommarceReadOnlyCtx,
            IUserRepository userRepository,
            IUserGroupRepository userGroupRepository,
            ICategoryRepository categoryRepository,
            IPermissionHeaderSettingsRepository permissionHeaderSettingsRepository,
            IPermissionRowSettingsRepository permissionRowSettingsRepository,
            IUserRoleRepository userRolesRepository,
            IAccesslistRepository accesslistRepository,
            IAccessMapperRepository accessMapperRepository,
            IModuleRepository moduleRepository,
            IPmsGroupRepository pmsGroupRepository,

           IRegistrationArchiveRepository registrationArchiveRepository,
           IRegistrationArchiveFileRepository registrationArchiveFileRepository,
           ICertificationRepository certificationRepository,
           ITaskTypeRepository taskTypeRepository,
           IMeetingTypeRepository meetingTypeRepository,
           IDesignationRepository designationRepository,
           IDocumentCategoriesRepository documentCategoriesRepository,
           ISkillRepository skillRepository,
           IClientRepository clientRepository,
           IClientLogRepository clientLogRepository,
           IProjectRequestRepository projectRequestRepository,
           IProjectRequestLogRepository projectRequestLogRepository,
           ITaskRepository taskRepository,
           ITaskLogRepository taskLogRepository,
           ITaskTimeTrackingRepository taskTimeTrackingRepository,


           IApprovalForProjectLogRepository approvalForProjectLogRepository,
           IApprovalForRegisteredClientLogRepository approvalForRegisteredClientLogRepository,
           ITestCaseRepository testCaseRepository,
           ITestCategoryRepository testCategoryRepository,
           ITestScenarioRepository testScenarioRepository,
           ITestStepRepository testStepRepository,
           IBugAndDefectRepository bugAndDefectRepository,
           IBugAndDefectFileRepository bugAndDefectFileRepository,
           IAgreementDocumentRepository agreementDocumentRepository,
           IMeetingRepository meetingRepository,

           IMeetingFilesRepository meetingFilesRepository,
           IAttendedUserMeetingRepository attendedUserMeetingRepository,
           IDocumentsRepository documentsRepository,
           ISecurityTestingRepository securityTestingRepository,
           ISecurityTestingFileRepository securityTestingFileRepository,
           IBugAndDefectLogRepository bugAndDefectLogRepository,
           ITestScopeRepository testScopeRepository,
           IHardwareTestingRepository hardwareTestingRepository,
           IAllTypesOfDocumentRepository allTypesOfDocumentRepository,

        //ProjectPackageConfiguration
            IProjectModuleNameRepository projectModuleNameRepository,
            IProjectPackageRepository projectPackageRepository,
            IProjectPricingSetupRepository projectPricingSetupRepository,
           IPaymentCalculationHeaderRepository paymentCalculationHeaderRepository,
           IPaymentCalculationRowRepository paymentCalculationRowRepository,
           IPaymentInformationRepository paymentInformationRepository,
           IReconciliationRepository reconciliationRepository,
           IDepositSlipFileRepository depositSlipFileRepository,
           IApprovalForAllDocumentRepository approvalForAllDocumentRepository,
           IProjectStateLogRepository projectStateLogRepository,
           IFeedbackRepository feedbackRepository,
           IDocAmendmentRepository docAmendmentRepository,
           IProjectCertificationRepository projectCertificationRepository,
           IReviewCommentRepository reviewCommentRepository,
           IDefaultDocContentRepository defaultDocContentRepository
        )

        {
            WriteOnlyCtx = ecommarceWriteOnlyCtx;
            ReadOnlyCtx = ecommarceReadOnlyCtx;
            DocumentsRepository = documentsRepository;
            BugAndDefectLogRepository = bugAndDefectLogRepository;
            users = userRepository;
            usergroups = userGroupRepository;
            Categoriess = categoryRepository;
            UserRoles = userRolesRepository;
            Accesslist = accesslistRepository;
            AccessMapper = accessMapperRepository;
            Module = moduleRepository;
            PmsGroup = pmsGroupRepository;
            PermissionHeaderSettingsRepository = permissionHeaderSettingsRepository;
            PermissionRowSettingsRepository = permissionRowSettingsRepository;

            RegistrationArchiveRepository = registrationArchiveRepository;
            RegistrationArchiveFileRepository = registrationArchiveFileRepository;
            CertificationRepository = certificationRepository;
            TaskTypeRepository = taskTypeRepository;
            MeetingTypeRepository = meetingTypeRepository;
            DesignationRepository = designationRepository;
            DocumentCategoriesRepository = documentCategoriesRepository;
            SkillRepository = skillRepository;
            ClientRepository = clientRepository;
            ClientLogRepository = clientLogRepository;
            ProjectRequestRepository = projectRequestRepository;
            ProjectRequestLogRepository = projectRequestLogRepository;
            TaskRepository = taskRepository;
            TaskLogRepository = taskLogRepository;
            TaskTimeTrackingRepository = taskTimeTrackingRepository;
            ApprovalForRegisteredClientLogRepository = approvalForRegisteredClientLogRepository;
            ApprovalForProjectLogRepository = approvalForProjectLogRepository;
            TestScenarioRepository = testScenarioRepository;
            TestCaseRepository = testCaseRepository;
            TestCategoryRepository = testCategoryRepository;
            TestStepRepository = testStepRepository;
            BugAndDefectFileRepository = bugAndDefectFileRepository;
            BugAndDefectRepository = bugAndDefectRepository;
            AgreementDocumentRepository = agreementDocumentRepository;
            MeetingRepository = meetingRepository;
            AttendedUserMeetingRepository = attendedUserMeetingRepository;
            MeetingFilesRepository = meetingFilesRepository;
            DocumentsRepository = documentsRepository;
            SecurityTestingFileRepository = securityTestingFileRepository;
            SecurityTestingRepository = securityTestingRepository;
            TestScopeRepository = testScopeRepository;
            AllTypesOfDocumentRepository = allTypesOfDocumentRepository;

            HardwareTestingRepository = hardwareTestingRepository;
            //ProjectPackageConfiguration
            ProjectModuleNameRepository = projectModuleNameRepository;
            ProjectPackageRepository = projectPackageRepository;
            ProjectPricingSetupRepository = projectPricingSetupRepository;
            PaymentCalculationHeaderRepository = paymentCalculationHeaderRepository;
            PaymentCalculationRowRepository = paymentCalculationRowRepository;
            PaymentInformationRepository = paymentInformationRepository;
            DepositSlipFileRepository = depositSlipFileRepository;
            ReconciliationRepository = reconciliationRepository;

            ApprovalForAllDocumentRepository = approvalForAllDocumentRepository;
            FeedbackRepository = feedbackRepository;
            ProjectStateLogRepository = projectStateLogRepository;
            ProjectCertificationRepository = projectCertificationRepository;
            DocAmendmentRepository = docAmendmentRepository;
            ReviewCommentRepository = reviewCommentRepository;
            DefaultDocContentRepository = defaultDocContentRepository;
        }


        public IDbContextTransaction Begin()
        {
            try
            {
                IDbContextTransaction transaction = WriteOnlyCtx.Database.BeginTransaction();
                return transaction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Complete(IDbContextTransaction transaction, CompletionState completionState)
        {
            try
            {
                if (transaction != null && transaction.TransactionId != null && transaction.GetDbTransaction() != null)
                {
                    if (completionState == CompletionState.Success)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch
            {
                transaction.Rollback();
            }
        }

        #region Select a Repository based on given type

        //public string GetEnumDisplayName(this Enum value)
        //{
        //    FieldInfo fi = value.GetType().GetField(value.ToString());

        //    DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

        //    if (attributes != null && attributes.Length > 0)
        //        return attributes[0].Name;
        //    else
        //        return value.ToString();
        //}

        public IBaseRepository<T> Select<T>(T entity) where T : BaseEntity
        {
            IBaseRepository<T> repository = default(IBaseRepository<T>);
            switch (entity)
            {
                case User _:
                    repository = (IBaseRepository<T>)users;
                    break;

                case UserGroup _:
                    repository = (IBaseRepository<T>)usergroups;
                    break;
                case Category _:
                    repository = (IBaseRepository<T>)Categoriess;
                    break;

                case UserRole _:
                    repository = (IBaseRepository<T>)UserRoles;
                    break;
                case Accesslist _:
                    repository = (IBaseRepository<T>)Accesslist;
                    break;

                case AccessMapper _:
                    repository = (IBaseRepository<T>)AccessMapper;
                    break;
                case Common.Entity.Module _:
                    repository = (IBaseRepository<T>)Module;
                    break;
                case PmsGroup _:
                    repository = (IBaseRepository<T>)PmsGroup;
                    break;

                case Certification _:
                    repository = (IBaseRepository<T>)CertificationRepository;
                    break;
                case Designation _:
                    repository = (IBaseRepository<T>)DesignationRepository;
                    break;
                case DocumentCategories _:
                    repository = (IBaseRepository<T>)DocumentCategoriesRepository;
                    break;
                case MeetingType _:
                    repository = (IBaseRepository<T>)MeetingTypeRepository;
                    break;
                case TaskType _:
                    repository = (IBaseRepository<T>)TaskTypeRepository;
                    break;
                case Skill _:
                    repository = (IBaseRepository<T>)SkillRepository;
                    break;

                // Archive
                case RegistrationArchive _:
                    repository = (IBaseRepository<T>)RegistrationArchiveRepository;
                    break;
                case RegistrationArchiveFile _:
                    repository = (IBaseRepository<T>)RegistrationArchiveFileRepository;
                    break;


                //PermissionSettings
                case PermissionHeaderSettings _:
                    repository = (IBaseRepository<T>)PermissionHeaderSettingsRepository;
                    break;
                case PermissionRowSettings _:
                    repository = (IBaseRepository<T>)PermissionRowSettingsRepository;
                    break;
                //client
                case Client _:
                    repository = (IBaseRepository<T>)ClientRepository;
                    break;
                case ClientLog _:
                    repository = (IBaseRepository<T>)ClientLogRepository;
                    break;
                case ApprovalForRegisteredClientLog _:
                    repository = (IBaseRepository<T>)ApprovalForRegisteredClientLogRepository;
                    break;
                //Project
                case ProjectRequest _:
                    repository = (IBaseRepository<T>)ProjectRequestRepository;
                    break;
                case ProjectRquestLog _:
                    repository = (IBaseRepository<T>)ProjectRequestLogRepository;
                    break;
                case TaskOfProject _:
                    repository = (IBaseRepository<T>)TaskRepository;
                    break;
                case TaskLog _:
                    repository = (IBaseRepository<T>)TaskLogRepository;
                    break;
                case TaskTimeTracking _:
                    repository = (IBaseRepository<T>)TaskTimeTrackingRepository;
                    break;
                case ApprovalForProjectLog _:
                    repository = (IBaseRepository<T>)ApprovalForProjectLogRepository;
                    break;
                case TestScenario _:
                    repository = (IBaseRepository<T>)TestScenarioRepository;
                    break;
                case TestCase _:
                    repository = (IBaseRepository<T>)TestCaseRepository;
                    break;
                case TestStep _:
                    repository = (IBaseRepository<T>)TestStepRepository;
                    break;
                case TestCategory _:
                    repository = (IBaseRepository<T>)TestCategoryRepository;
                    break;
                case BugAndDefect _:
                    repository = (IBaseRepository<T>)BugAndDefectRepository;
                    break;
                case BugAndDefectFile _:
                    repository = (IBaseRepository<T>)BugAndDefectFileRepository;
                    break;
                case AgreementDocuments _:
                    repository = (IBaseRepository<T>)AgreementDocumentRepository;
                    break;
                case Meeting _:
                    repository = (IBaseRepository<T>)MeetingRepository;
                    break;
                case AttendedUserMeeting _:
                    repository = (IBaseRepository<T>)AttendedUserMeetingRepository;
                    break;
                case MeetingFiles _:
                    repository = (IBaseRepository<T>)MeetingFilesRepository;
                    break;
                case DocumentsByType _:
                    repository = (IBaseRepository<T>)DocumentsRepository;
                    break;
                case SecurityTesting _:
                    repository = (IBaseRepository<T>)SecurityTestingRepository;
                    break;
                case SecurityTestingFile _:
                    repository = (IBaseRepository<T>)SecurityTestingFileRepository;
                    break;
                case ProjectPricingSetup _:
                    repository = (IBaseRepository<T>)ProjectPricingSetupRepository;
                    break;
                case BugAndDefectLog _:
                    repository = (IBaseRepository<T>)BugAndDefectLogRepository;
                    break;
                    //HardWareTesting
                case TestScope _:
                    repository = (IBaseRepository<T>)TestScopeRepository;
                    break;
                case ProjectModuleName _:
                    repository = (IBaseRepository<T>)ProjectModuleNameRepository;
                    break;
                case ProjectPackage _:
                    repository = (IBaseRepository<T>)ProjectPackageRepository;
                    break;
            
                case PaymentCalculationHeader _:
                    repository = (IBaseRepository<T>)PaymentCalculationHeaderRepository;
                    break;
                case PaymentCalculationRow _:
                    repository = (IBaseRepository<T>)PaymentCalculationRowRepository;
                    break;
                case HardwareTesting _:
                    repository = (IBaseRepository<T>)HardwareTestingRepository;
                    break;
                case AllTypesOfDocument _:
                    repository = (IBaseRepository<T>)AllTypesOfDocumentRepository;
                    break;
                case PaymentInformation _:
                    repository = (IBaseRepository<T>)PaymentInformationRepository;
                    break;
                case DepositSlipFile _:
                    repository = (IBaseRepository<T>)DepositSlipFileRepository;
                    break;
                case Reconciliation _:
                    repository = (IBaseRepository<T>)ReconciliationRepository;
                    break;
                case ApprovalForAllDocument _:
                    repository = (IBaseRepository<T>)ApprovalForAllDocumentRepository;
                    break;
                case Feedback _:
                    repository = (IBaseRepository<T>)FeedbackRepository;
                    break;

                case ProjectStateLog _:
                    repository = (IBaseRepository<T>)ProjectStateLogRepository;
                    break;

                case DocumentAmendment _:
                    repository = (IBaseRepository<T>)DocAmendmentRepository;
                    break;
                case ProjectCertification _:
                    repository = (IBaseRepository<T>)ProjectCertificationRepository;
                    break;
                case ReviewComment _:
                    repository = (IBaseRepository<T>)ReviewCommentRepository;
                    break;
                case DefaultDocumentContent _:
                    repository = (IBaseRepository<T>)DefaultDocContentRepository;
                    break;
            }
            return repository;
        }


        public IBaseRepository<T> Select<T>() where T : BaseEntity
        {

            IBaseRepository<T> repository = default(IBaseRepository<T>);

            Type type = typeof(T);

            if (type == typeof(User))
            {
                repository = (IBaseRepository<T>)users;
            }
            else if (type == typeof(UserGroup))
            {
                repository = (IBaseRepository<T>)usergroups;
            }
            else if (type == typeof(Category))
            {
                repository = (IBaseRepository<T>)Categoriess;
            }

            else if (type == typeof(UserRole))
            {
                repository = (IBaseRepository<T>)UserRoles;
            }
            else if (type == typeof(Accesslist))
            {
                repository = (IBaseRepository<T>)Accesslist;
            }
            else if (type == typeof(AccessMapper))
            {
                repository = (IBaseRepository<T>)AccessMapper;
            }
            else if (type == typeof(Common.Entity.Module))
            {
                repository = (IBaseRepository<T>)Module;
            }

            else if (type == typeof(PmsGroup))
            {
                repository = (IBaseRepository<T>)PmsGroup;
            }

            // Archive
            else if (type == typeof(RegistrationArchive))
            {
                repository = (IBaseRepository<T>)RegistrationArchiveRepository;
            }
            else if (type == typeof(RegistrationArchiveFile))
            {
                repository = (IBaseRepository<T>)RegistrationArchiveFileRepository;
            }

            //PermissionSettings
            else if (type == typeof(PermissionHeaderSettings))
            {
                repository = (IBaseRepository<T>)PermissionHeaderSettingsRepository;
            }
            else if (type == typeof(PermissionRowSettings))
            {
                repository = (IBaseRepository<T>)PermissionRowSettingsRepository;
            }
            //GeneralSetup
            else if (type == typeof(Designation))
            {
                repository = (IBaseRepository<T>)DesignationRepository;
            }
            else if (type == typeof(Certification))
            {
                repository = (IBaseRepository<T>)CertificationRepository;
            }
            else if (type == typeof(DocumentCategories))
            {
                repository = (IBaseRepository<T>)DocumentCategoriesRepository;
            }
            else if (type == typeof(TaskType))
            {
                repository = (IBaseRepository<T>)TaskTypeRepository;
            }
            else if (type == typeof(MeetingType))
            {
                repository = (IBaseRepository<T>)MeetingTypeRepository;
            }
            else if (type == typeof(Skill))
            {
                repository = (IBaseRepository<T>)SkillRepository;
            }
            //client
            else if (type == typeof(Client))
            {
                repository = (IBaseRepository<T>)ClientRepository;
            }
            else if (type == typeof(ClientLog))
            {
                repository = (IBaseRepository<T>)ClientLogRepository;
            }
            else if (type == typeof(ApprovalForRegisteredClientLog))
            {
                repository = (IBaseRepository<T>)ApprovalForRegisteredClientLogRepository;
            }
            //project
            else if (type == typeof(ProjectRequest))
            {
                repository = (IBaseRepository<T>)ProjectRequestRepository;
            }
            else if (type == typeof(ProjectRquestLog))
            {
                repository = (IBaseRepository<T>)ProjectRequestLogRepository;
            }
            else if (type == typeof(TaskOfProject))
            {
                repository = (IBaseRepository<T>)TaskRepository;
            }
            else if (type == typeof(TaskLog))
            {
                repository = (IBaseRepository<T>)TaskLogRepository;
            }
            else if (type == typeof(TaskTimeTracking))
            {
                repository = (IBaseRepository<T>)TaskTimeTrackingRepository;
            }
            else if (type == typeof(ApprovalForProjectLog))
            {
                repository = (IBaseRepository<T>)ApprovalForProjectLogRepository;
            }
            else if (type == typeof(TestScenario))
            {
                repository = (IBaseRepository<T>)TestScenarioRepository;
            }
            else if (type == typeof(TestCase))
            {
                repository = (IBaseRepository<T>)TestCaseRepository;
            }
            else if (type == typeof(TestCategory))
            {
                repository = (IBaseRepository<T>)TestCategoryRepository;
            }
            else if (type == typeof(TestStep))
            {
                repository = (IBaseRepository<T>)TestStepRepository;
            }
            else if (type == typeof(BugAndDefect))
            {
                repository = (IBaseRepository<T>)BugAndDefectRepository;
            }
            else if (type == typeof(BugAndDefectFile))
            {
                repository = (IBaseRepository<T>)BugAndDefectFileRepository;
            }
            else if (type == typeof(AgreementDocuments))
            {
                repository = (IBaseRepository<T>)AgreementDocumentRepository;
            }
            else if (type == typeof(Meeting))
            {
                repository = (IBaseRepository<T>)MeetingRepository;
            }
            else if (type == typeof(AttendedUserMeeting))
            {
                repository = (IBaseRepository<T>)AttendedUserMeetingRepository;
            }
            else if (type == typeof(MeetingFiles))
            {
                repository = (IBaseRepository<T>)MeetingFilesRepository;
            }
            else if (type == typeof(DocumentsByType))
            {
                repository = (IBaseRepository<T>)DocumentsRepository;
            }
            else if (type == typeof(SecurityTesting))
            {
                repository = (IBaseRepository<T>)SecurityTestingRepository;
            }
            else if (type == typeof(SecurityTestingFile))
            {
                repository = (IBaseRepository<T>)SecurityTestingFileRepository;
            }
            else if (type == typeof(ProjectPackage))
            {
                repository = (IBaseRepository<T>)ProjectPackageRepository;
            }
            else if (type == typeof(ProjectPricingSetup))
            {
                repository = (IBaseRepository<T>)ProjectPricingSetupRepository;
            }
            //HardWareTesting
            else if (type == typeof(TestScope))
            {
                repository = (IBaseRepository<T>)TestScopeRepository;
            }
            else if (type == typeof(HardwareTesting))
            {
                repository = (IBaseRepository<T>)HardwareTestingRepository;
            }
            else if (type == typeof(ProjectModuleName))
            {
                repository = (IBaseRepository<T>)ProjectModuleNameRepository;
            }
            else if (type == typeof(ProjectPackage))
            {
                repository = (IBaseRepository<T>)ProjectPackageRepository;
            }
            else if (type == typeof(PaymentCalculationHeader))
            {
                repository = (IBaseRepository<T>)PaymentCalculationHeaderRepository;
            }
            else if (type == typeof(PaymentCalculationRow))
            {
                repository = (IBaseRepository<T>)PaymentCalculationRowRepository;
            }
            else if (type == typeof(AllTypesOfDocument))
            {
                repository = (IBaseRepository<T>)AllTypesOfDocumentRepository;
            }
            else if (type == typeof(PaymentInformation))
            {
                repository = (IBaseRepository<T>)PaymentInformationRepository;
            }
            else if (type == typeof(DepositSlipFile))
            {
                repository = (IBaseRepository<T>)DepositSlipFileRepository;
            }
            else if (type == typeof(Reconciliation))
            {
                repository = (IBaseRepository<T>)ReconciliationRepository;
            }
            else if (type == typeof(ApprovalForAllDocument))
            {
                repository = (IBaseRepository<T>)ApprovalForAllDocumentRepository;
            }
            else if (type == typeof(Feedback))
            {
                repository = (IBaseRepository<T>)FeedbackRepository;
            }

            else if (type == typeof(ProjectStateLog))
            {
                repository = (IBaseRepository<T>)ProjectStateLogRepository;
            }

            else if (type == typeof(DocumentAmendment))
            {
                repository = (IBaseRepository<T>)DocAmendmentRepository;
            }
            else if (type == typeof(ProjectCertification))
            {
                repository = (IBaseRepository<T>)ProjectCertificationRepository;
            }
            else if (type == typeof(ReviewComment))
            {
                repository = (IBaseRepository<T>)ReviewCommentRepository;
            }
            else if (type == typeof(DefaultDocumentContent))
            {
                repository = (IBaseRepository<T>)DefaultDocContentRepository;
            }
            return repository;
        }


        public virtual async Task<(ExecutionState executionState, string message)> SaveAsync(IDbContextTransaction transaction)
        {
            if (transaction != null)
            {
                if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                {
                    try
                    {
                        await WriteOnlyCtx.SaveChangesAsync();
                        return (executionState: ExecutionState.Success, message: "Transaction completed.");
                    }
                    catch (Exception ex)
                    {
                        return (executionState: ExecutionState.Failure, message: ex.Message);
                    }
                }
                else
                {
                    return (executionState: ExecutionState.Failure, message: "Transaction not found.");
                }
            }
            else
            {
                return (executionState: ExecutionState.Failure, message: "Transaction not found.");
            }
        }
        public async Task<(ExecutionState executionState, T entity, string message)> CreateAsync<T>(T entity) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select(entity);

            (ExecutionState executionState, T entity, string message) createdEntity = await repository.CreateAsync(entity);

            return createdEntity;
        }
        public async Task<(ExecutionState executionState, T entity, string message)> GetAsync<T>(long id) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();
            (ExecutionState executionState, T entity, string message) retrievedEntity = await repository.GetAsync(id);
            return retrievedEntity;
        }
        public async Task<(ExecutionState executionState, T entity, string message)> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select(entity);
            (ExecutionState executionState, T entity, string message) updateEntity = await repository.UpdateAsync(entity);
            return updateEntity;
        }
        public async Task<(ExecutionState executionState, T entity, string message)> RemoveAsync<T>(long id) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();
            (ExecutionState executionState, T entity, string message) removeEntity = await repository.RemoveAsync(id);
            return removeEntity;
        }
        public async Task<(ExecutionState executionState, T entity, string message)> GetAsync<T>
            (FilterOptions<T> filterOptions, RetrievalPurpose retrievalPurpose = RetrievalPurpose.Consumption)
            where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState retrievedEntityExecutionState, T retrievedEntity, string retrievedEntityMessage) =
                await repository.GetAsync(filterOptions, retrievalPurpose);

            return (executionState: retrievedEntityExecutionState, entity: retrievedEntity, message: retrievedEntityMessage);
        }
        public async Task<(ExecutionState executionState, IQueryable<T> entity, string message)> List<T>(QueryOptions<T> queryOptions = null) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState retrievedEntitiesExecutionState, IQueryable<T> retrievedEntities, string retrievedEntitiesMessage) =
                await repository.List(queryOptions);

            return (executionState: retrievedEntitiesExecutionState, entity: retrievedEntities, message: retrievedEntitiesMessage);
        }
        public async Task<(ExecutionState executionState, string message)> DoesExistAsync<T>(FilterOptions<T> filterOptions)
            where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState entitityExistsExecutionState, string entitityExistsMessage) =
                await repository.DoesExistAsync(filterOptions);

            return (executionState: entitityExistsExecutionState, message: entitityExistsMessage);
        }
        public async Task<(ExecutionState executionState, long entityCount, string message)> CountAsync<T>(CountOptions<T> countOptions = null)
            where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState entitiesCountExecutionState, long entitiesCount, string entityCountMessage) =
                await repository.CountAsync(countOptions);

            return (executionState: entitiesCountExecutionState, entityCount: entitiesCount, message: entityCountMessage);
        }
        public async Task<(ExecutionState executionState, T entity, string message)> MarkAsActiveAsync<T>(long id) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();
            (ExecutionState executionState, T entity, string message) activeEntity = await repository.MarkAsActiveAsync(id);
            return activeEntity;
        }
        public async Task<(ExecutionState executionState, T entity, string message)> MarkAsInactiveAsync<T>(long id) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();
            (ExecutionState executionState, T entity, string message) inactiveEntity = await repository.MarkAsInactiveAsync(id);
            return inactiveEntity;
        }
        public async Task<(ExecutionState executionState, string message)> DoesExistAsync<T>(long id) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();
            (ExecutionState executionState, string message) doesExist = await repository.DoesExistAsync(id);
            return doesExist;
        }

        #endregion

        public void Dispose()
        {
            WriteOnlyCtx?.Dispose();
            ReadOnlyCtx?.Dispose();

            GC.SuppressFinalize(this);
        }

        public Task<(ExecutionState executionState, bool isDeleted, string message)> SoftDeleteAsync<T>(long id, long userId) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            return repository.SoftDeleteAsync(id, userId);
        }

        public static implicit operator GENERICUnitOfWork(GENERICReadOnlyCtx v)
        {
            throw new NotImplementedException();
        }
    }
}
