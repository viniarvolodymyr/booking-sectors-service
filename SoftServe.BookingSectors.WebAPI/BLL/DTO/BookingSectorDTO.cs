using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class BookingSectorDTO
    {
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public bool? IsApproved { get; set; }
        public int CreateUserId { get; set; }
    }
}
