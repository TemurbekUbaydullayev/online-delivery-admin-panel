using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFoodOrdering.Domain.Common
{
    public class PaginationParameters
    {
        private const int maxPageSize = 20;
        private int pageSize;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > maxPageSize ? maxPageSize : value;
        }
        public int PageIndex { get; set; }
    }
}
