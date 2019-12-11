using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual ICollection<User> User { get; set; } = new HashSet<User>();
    }
}
