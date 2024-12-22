
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.ProjectPackageConfiguration;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Meetings;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels
{
    public class AdminDashBoardVM
    {
        public int? TotalRegistredClients {  get; set; }
        public int? TotalCompletedProjects {  get; set; }
        public int? TotalEmployees {  get; set; }
        public int? TotalAgreements {  get; set; }
        public int? PendingProjectRequests {  get; set; }
        public int? totaltestRequest {  get; set; }
        public int? RejectedProjectRequests { get; set; }   
        public List<TaskVM>? TaskOfProjects { get; set; }
        public List<PaymentCalculationHeaderVM>? paymentInformation { get; set; }
        public List<MeetingVM>? MetingInformation { get; set; }
        public string? UserName { get;set; }
    }
}
