using System;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public partial class TournamentSector
    {
        public int Id { get; set; }
        public int IdTournament { get; set; }
        public int IdSectors { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual Sector IdSectorsNavigation { get; set; }
        public virtual Tournament IdTournamentNavigation { get; set; }
    }
}
