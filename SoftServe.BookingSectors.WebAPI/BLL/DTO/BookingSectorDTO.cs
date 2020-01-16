using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class BookingSectorDTO
    {
        public int Id { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public bool? IsApproved { get; set; }
        public int SectorId { get; set; }
        public int UserId { get; set; }
        public int? TournamentId { get; set; }
        public int CreateUserId { get; set; }
    }
}
