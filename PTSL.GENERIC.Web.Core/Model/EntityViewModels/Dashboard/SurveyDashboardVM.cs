using PTSL.GENERIC.Web.Core.Helper.Enum;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Dashboard;

public class SurveyDashboardVM
{
    
    public string? PhoneNumber { get; set; }
    public string? NidNo { get; set; }
    public Gender Gender { get; set; }
    public double TotalAIGLoanTaken { get; set; }
    public double TotalAIGLoanRepayment { get; set; }
    public double TotalTotalSaving { get; set; }
    public int TotalMeetingParticipations { get; set; }
    public int TotalTrainingParticipations { get; set; }
}
