using System.Collections.Generic;
using System.Linq;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class PagedBookingsList<T> : List<T>
    {
        public int totalCount { get; private set; }
        public int pageIndex { get; private set; }
        public int pageSize { get; private set; }

        public PagedBookingsList(List<T> items, int totalCount, int pageIndex, int pageSize)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.totalCount = totalCount;

            AddRange(items);
        }
        public static PagedBookingsList<T> toPagedList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            pageIndex++;
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagedBookingsList<T>(items, count, pageIndex, pageSize);
        }
    }
}
       
