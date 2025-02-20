﻿using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Interfaces;
using Serialize.Linq.Nodes;

namespace PTSL.GENERIC.Common.QuerySerialize.Implementation
{
    public class FilterOptionsNodes : BaseSerializeLinq, IFilterOptionsNodes
    {
        public ExpressionNode IncludeExpressionNode { get; set; }
        public ExpressionNode FilterExpressionNode { get; set; }
        public List ListCondition { get; set; }
    }
}