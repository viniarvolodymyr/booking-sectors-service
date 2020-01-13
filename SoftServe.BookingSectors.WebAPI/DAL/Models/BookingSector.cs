using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public partial class BookingSector
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SectorId { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }
        public int? TournamentId { get; set; }

        public virtual Sector Sector { get; set; }
        public virtual Tournament Tournament { get; set; }
        public virtual User User { get; set; }
    }
}
