using PTSL.GENERIC.Common.Enum;
using Serialize.Linq.Nodes;

namespace PTSL.GENERIC.Common.QuerySerialize.Interfaces
{
    public interface ICountOptionsNodes
    {
        public ExpressionNode IncludeExpressionNode { get; set; }
        public ExpressionNode FilterExpressionNode { get; set; }
        public List ListCondition { get; set; }
    }
}