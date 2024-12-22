using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using Serialize.Linq.Nodes;

namespace PTSL.GENERIC.Common.QuerySerialize.Interfaces
{
    public interface IQueryOptionsNodes
    {
        public ExpressionNode IncludeExpressionNode { get; set; }
        public ExpressionNode FilterExpressionNode { get; set; }
        public ExpressionNode SortingExpressionNode { get; set; }
        public Pagination Pagination { get; set; }
        public List ListCondition { get; set; }
    }
}