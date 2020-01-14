using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftServe.BookingSectors.WebAPI.Tests.ServicesTests.Data
{
    public static class BookingSectorData
    {
        public static List<BookingSector> CreateBookingSectorsList()
        {
            return new List<BookingSector>()
            {
                new BookingSector()
                {
                    Id = 1,
                    UserId = 1,
                    SectorId = 1,
                    BookingStart = new DateTime(2020, 1, 9),
                    BookingEnd = new DateTime(2020, 1, 10),
                    IsApproved = false,
                    CreateDate = new DateTime(2020, 1, 9),
                    CreateUserId = 1,
                    ModDate = new DateTime(2020, 1, 9)
                },
                new BookingSector()
                {
                    Id = 2,
                    UserId = 2,
                    SectorId = 2,
                    BookingStart = new DateTime(2020, 1, 13),
                    BookingEnd = new DateTime(2020, 1, 16),
                    IsApproved = true,
                    TournamentId = 2,
                    CreateDate = new DateTime(2020, 1, 13),
                    CreateUserId = 2,
                    ModDate = new DateTime(2020, 1, 13)
                },
                new BookingSector()
                {
                    Id = 3,
                    UserId = 3,
                    SectorId = 3,
                    BookingStart = new DateTime(2020, 1, 18),
                    BookingEnd = new DateTime(2020, 1, 21),
                    IsApproved = false,
                    CreateDate = new DateTime(2020, 1, 18),
                    CreateUserId = 3,
                    ModDate = new DateTime(2020, 1, 18)
                },
                new BookingSector()
                {
                    Id = 4,
                    UserId = 4,
                    SectorId = 4,
                    BookingStart = new DateTime(2020, 1, 26),
                    BookingEnd = new DateTime(2020, 1, 30),
                    IsApproved = true,
                    CreateDate = new DateTime(2020, 1, 26),
                    CreateUserId = 3,
                    ModDate = new DateTime(2020, 1, 26)
                }
            };
        }
        public static BookingSectorDTO CreateBookingSectorDTO()
        {
            return new BookingSectorDTO()
            {
                Id = 10,
                UserId = 2,
                SectorId = 2,
                BookingStart = new DateTime(2020, 1, 9),
                BookingEnd = new DateTime(2020, 1, 10),
                IsApproved = false,
                CreateUserId = 2
            };
        }
    }
}
