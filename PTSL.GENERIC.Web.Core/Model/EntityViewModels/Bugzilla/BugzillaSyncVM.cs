using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Bugzilla
{
    public class BugzillaSyncVM
    {
        public BugzillaSyncVM()
        {
            BugzillaResolvedBugs = new List<Bug>();
            MatchedBugsToUpdate = new List<BugAndDefectVM>();
        }
        public List<Bug> BugzillaResolvedBugs { get; set; }
        public List<BugAndDefectVM> MatchedBugsToUpdate { get; set; }
    }
}
