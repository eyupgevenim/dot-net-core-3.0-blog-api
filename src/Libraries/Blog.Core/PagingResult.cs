using System.Collections.Generic;
using System.Linq;

namespace Blog.Core
{
    public class PagingResult<T> where T : BaseEntity
    {
        public PagingResult(IQueryable<T> entity, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            if(pageSize == 0)
            {
                Items = new List<T>();
                return;
            }

            TotalCount = entity.Count();
            TotalPages = TotalCount / pageSize;
            if (TotalCount % pageSize > 0) TotalPages++;
            Items = entity.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public List<T> Items { get; private set; }
    }
}
