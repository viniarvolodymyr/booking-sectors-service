using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class TournamentSectorDTO
    {
        public int Id { get; set; }
        public int IdTournament { get; set; }
        public int IdSectors { get; set; }
    }
}
