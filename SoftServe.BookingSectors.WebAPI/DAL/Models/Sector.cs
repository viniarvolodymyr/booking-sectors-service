using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public partial class Sector
    {
        public Sector()
        {
            BookingSector = new HashSet<BookingSector>();
            TournamentSector = new HashSet<TournamentSector>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public decimal GpsLat { get; set; }
        public decimal GpsLng { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual ICollection<BookingSector> BookingSector { get; set; }
        public virtual ICollection<TournamentSector> TournamentSector { get; set; }
    }
}
