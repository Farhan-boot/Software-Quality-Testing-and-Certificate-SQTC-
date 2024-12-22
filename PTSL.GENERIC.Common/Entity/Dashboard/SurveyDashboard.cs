namespace PTSL.GENERIC.Common.Entity.Dashboard;

public class SurveyDashboard
{
    public long SurveyId { get; set; }
    public string? BeneficiaryName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? NidNo { get; set; }
    public Gender Gender { get; set; }
    public double TotalAIGLoanTaken { get; set; }
    public double TotalAIGLoanRepayment { get; set; }
    public double TotalTotalSaving { get; set; }
    public int TotalMeetingParticipations { get; set; }
    public int TotalTrainingParticipations { get; set; }
}
