﻿using System;

namespace SoftServe.BookingSectors.WebAPI.BLL.DTO
{
    public class TournamentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PreparationTerm { get; set; }
    }
}
