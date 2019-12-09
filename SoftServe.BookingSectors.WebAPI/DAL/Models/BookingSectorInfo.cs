using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public sealed class BookingSectorInfo
    {
        public int SectorId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int UserId { get; set; }

    }
}
