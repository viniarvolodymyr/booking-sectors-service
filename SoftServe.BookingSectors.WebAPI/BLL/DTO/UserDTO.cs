using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class UserDTO
    {
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Phone { get; set; }
        public byte[] Password { get; set; }
        public string Role { get; set; }
        public byte[] Photo { get; set; }
    }
}
