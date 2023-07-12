using System.Collections.Generic;

namespace Common
{
    public class PagedCollectionResponse<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalResult { get; set; }
    }
}
