namespace PTSL.GENERIC.Web.Core.Model;

public class PaginationResutlVM<T>
{
    public int iTotalRecords { get; set; }
    public int iTotalDisplayRecords { get; set; }
    public IList<T> aaData { get; set; }
}
