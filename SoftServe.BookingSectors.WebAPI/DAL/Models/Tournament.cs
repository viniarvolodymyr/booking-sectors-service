using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            BookingSector = new HashSet<BookingSector>();
            TournamentSector = new HashSet<TournamentSector>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int PreparationTerm { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual ICollection<BookingSector> BookingSector { get; set; }
        public virtual ICollection<TournamentSector> TournamentSector { get; set; }
    }
}
