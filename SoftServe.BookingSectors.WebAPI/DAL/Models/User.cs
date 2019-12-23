using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public partial class User
    {
        public User()
        {
            BookingSector = new HashSet<BookingSector>();
            TokenCreate = new HashSet<Token>();
            TokenMod = new HashSet<Token>();
        }

        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public byte[] Password { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }
        public byte[] Photo { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual ICollection<BookingSector> BookingSector { get; set; }
        public virtual ICollection<Token> TokenCreate { get; set; }
        public virtual ICollection<Token> TokenMod { get; set; }
    }
}
