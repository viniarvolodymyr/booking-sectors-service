using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class TournamentSectorDTO
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int SectorsId { get; set; }
    }
}
