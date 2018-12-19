using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstExample.Core.Entities.Dto
{
    public class Pagination<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PagInfo PageInfo { get; set; }

    }
}