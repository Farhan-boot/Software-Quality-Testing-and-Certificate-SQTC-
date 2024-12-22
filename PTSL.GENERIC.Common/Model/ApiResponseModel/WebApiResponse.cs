using PTSL.GENERIC.Common.Enum;

namespace PTSL.GENERIC.Common.Model
{
    public class WebApiResponse<T>
    {
        public WebApiResponse()
        { }

        public WebApiResponse((ExecutionState executionState, T entity, string message) result)
        {
            ExecutionState = result.executionState;
            Data = result.entity;
            Message = result.message;
        }

        public WebApiResponse((ExecutionState executionState, string message) result)
        {
            ExecutionState = result.executionState;
            Message = result.message;
        }

        public ExecutionState ExecutionState { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

    }
}
