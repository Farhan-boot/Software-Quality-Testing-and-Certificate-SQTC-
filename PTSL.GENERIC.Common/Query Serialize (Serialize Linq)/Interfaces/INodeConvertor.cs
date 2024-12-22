using PTSL.GENERIC.Common.QuerySerialize.Interfaces;

namespace PTSL.GENERIC.Common.QuerySerialize.Interfaces
{
    // TODO: develop a class for this as well
    public interface INodeConvertor<TEntity>
    {
        public IQueryOptions<TEntity> ToQueryOptions(IQueryOptionsNodes queryOptionNodes);
        public IFilterOptions<TEntity> ToFilterOptions(IFilterOptionsNodes filterOptionNodes);
        public ICountOptions<TEntity> ToCountOptions(ICountOptionsNodes countOptionNodes);
    }
}