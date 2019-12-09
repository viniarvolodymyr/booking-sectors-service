using System;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class TournamentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int PreparationTerm { get; set; }
        public int CreateUserId { get; set; }
        public int? ModUserId { get; set; }
    }
}
