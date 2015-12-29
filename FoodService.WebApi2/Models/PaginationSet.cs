using System.Collections.Generic;
using System.Linq;

namespace FoodService.WebApi2.Models
{

        public class PaginationSet<T>
        {
            public int Page { get; set; }
            public int TotalPages { get; set; }
            public int TotalCount { get; set; }

            public IEnumerable<T> Items { get; set; }
        }
    
}