using System.Collections.Generic;

namespace E_Forester.Application.Pagination.Wrappers
{
    public class Page<T>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? TotalCount { get; set; }
        public ICollection<T> Data { get; set; }

        public Page(ICollection<T> data, int? pageIndex, int? pageSize, int? totalCount)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
