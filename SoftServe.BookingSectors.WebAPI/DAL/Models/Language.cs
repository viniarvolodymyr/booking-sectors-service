using System;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public partial class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }
    }
}
