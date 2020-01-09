using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.DAL.Models
{
    public partial class Email
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email1 { get; set; }
    }
}
