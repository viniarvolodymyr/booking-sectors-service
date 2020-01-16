using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            BookingSector = new HashSet<BookingSector>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int PreparationTerm { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }
        public string Description { get; set; }
        public DateTime TournamentStart { get; set; }
        public DateTime TournamentEnd { get; set; }

        public virtual ICollection<BookingSector> BookingSector { get; set; }
    }
}
