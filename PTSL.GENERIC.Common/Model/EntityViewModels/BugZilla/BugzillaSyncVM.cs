using PTSL.GENERIC.Common.Model.EntityViewModels.BugZilla;
using PTSL.GENERIC.Common.Model.EntityViewModels.Project;

namespace PTSL.GENERIC.Common.Model.EntityViewModels.Bugzilla
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
