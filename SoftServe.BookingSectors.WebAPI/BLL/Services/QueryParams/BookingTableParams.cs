using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services.QueryParams
{
    public class BookingTableParams
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public bool isExpired { get; set; }
        public bool? isApproved { get; set; }
    }
}
