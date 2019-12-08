using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class TournamentDTO
    {
        public TournamentDTO() {
            TournamentSector = new List<TournamentSectorDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int PreparationTerm { get; set; }
        public int CreateUserId { get; set; }
        public int? ModUserId { get; set; }
        public List<TournamentSectorDTO> TournamentSector { get; set; }
    }
}
