using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class SectorDTO
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public decimal GpsLat { get; set; }
        public decimal GpsLng { get; set; }
        public bool IsActive { get; set; }
        public int CreateUserId { get; set; }
    }
}
