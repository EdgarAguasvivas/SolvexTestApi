using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Pagination
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public static PaginatedResult<T> Success(List<T> items, int totalItems, int page, int pageSize) =>
            new() { Items = items, TotalItems = totalItems, Page = page, PageSize = pageSize };
    }
}
