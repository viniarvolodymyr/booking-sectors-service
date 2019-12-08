using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public partial class TournamentSector
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int SectorsId { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual Sector IdSectorsNavigation { get; set; }
        public virtual Tournament IdTournamentNavigation { get; set; }
    }
}
