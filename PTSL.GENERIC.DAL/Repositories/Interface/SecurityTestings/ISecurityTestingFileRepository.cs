using PTSL.GENERIC.Common.Entity.SecurityTestings;

namespace PTSL.GENERIC.DAL.Repositories.Interface.SecurityTestingFiles
{
    public interface ISecurityTestingFileRepository : IBaseRepository<SecurityTestingFile>
    {
        //Task<(ExecutionState executionState, IList<SecurityTestingFile> entity, string message)> Search(long? ProjectRequestId,long? TaskOfProjectId);
    }
}
