﻿using System;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }

        public int CreateUserId { get; set; }
        public int? ModUserId { get; set; }
        //public byte[] Photo { get; set; }
        //public DateTime ModDate { get; set; }
    }
}
