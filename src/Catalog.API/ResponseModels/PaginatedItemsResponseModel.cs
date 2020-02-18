using System.Collections.Generic;

namespace Catalog.API.ResponseModels
{
    public class PaginatedItemsResponseModel<TEntity> where TEntity : class
    {
        public PaginatedItemsResponseModel(int pageIndex, int pageSize, long total, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = total;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Total { get; set; }
        public IEnumerable<TEntity> Data { get; }
    }
}
