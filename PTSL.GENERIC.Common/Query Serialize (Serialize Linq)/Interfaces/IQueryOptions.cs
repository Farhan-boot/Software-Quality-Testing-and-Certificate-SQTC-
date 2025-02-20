﻿using Microsoft.EntityFrameworkCore.Query;
using PTSL.GENERIC.Common.Enum;
using PTSL.GENERIC.Common.QuerySerialize.Implementation;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PTSL.GENERIC.Common.QuerySerialize.Interfaces
{
    public interface IQueryOptions<TEntity>
    {
        public Expression<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> IncludeExpression { get; set; }
        public Expression<Func<TEntity, bool>> FilterExpression { get; set; }
        public Expression<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>> SortingExpression { get; set; }
        public Pagination Pagination { get; set; }
        public List ListCondition { get; set; }

        //public QueryOptions<TEntity> FromQueryOptionsNodes (QueryOptionsNodes nodes);
    }
}