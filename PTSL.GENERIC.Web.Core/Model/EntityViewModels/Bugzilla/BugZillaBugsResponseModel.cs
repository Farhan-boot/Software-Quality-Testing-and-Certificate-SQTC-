namespace PTSL.GENERIC.Web.Core.Model.EntityViewModels.Bugzilla
{
    public class BugZillaBugsResponseModel
    {
        public List<Bug> bugs { get; set; }
    }

    public class AssignedToDetail
    {
        public string email { get; set; }
        public string real_name { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Bug
    {
        public string priority { get; set; }
        public AssignedToDetail assigned_to_detail { get; set; }
        public List<object> blocks { get; set; }
        public string creator { get; set; }
        public DateTime last_change_time { get; set; }
        public bool is_cc_accessible { get; set; }
        public List<object> keywords { get; set; }
        public CreatorDetail creator_detail { get; set; }
        public List<string> cc { get; set; }
        public string url { get; set; }
        public string assigned_to { get; set; }
        public List<string> groups { get; set; }
        public List<object> see_also { get; set; }
        public int id { get; set; }
        public string whiteboard { get; set; }
        public DateTime creation_time { get; set; }
        public string qa_contact { get; set; }
        public List<object> depends_on { get; set; }
        public object dupe_of { get; set; }
        public int estimated_time { get; set; }
        public int remaining_time { get; set; }
        public string cf_rca { get; set; }
        public string resolution { get; set; }
        public string classification { get; set; }
        public List<object> alias { get; set; }
        public string op_sys { get; set; }
        public string status { get; set; }
        public List<CcDetail> cc_detail { get; set; }
        public string summary { get; set; }
        public bool is_open { get; set; }
        public string platform { get; set; }
        public string severity { get; set; }
        public List<object> flags { get; set; }
        public string version { get; set; }
        public object deadline { get; set; }
        public string component { get; set; }
        public int actual_time { get; set; }
        public bool is_creator_accessible { get; set; }
        public string product { get; set; }
        public bool is_confirmed { get; set; }
        public string target_milestone { get; set; }
    }

    public class CcDetail
    {
        public string email { get; set; }
        public string real_name { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class CreatorDetail
    {
        public string email { get; set; }
        public string real_name { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }
}
