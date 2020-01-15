using AutoMapper;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Mapping;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.Tests.Data
{
    public static class SectorData
    {
        private static MapperConfiguration mapperConfiguration;
        private static IMapper mapper;
        static SectorData()
        {
            mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<SectorProfile>();
            });
            mapper = mapperConfiguration.CreateMapper();
        }

        public static List<SectorDTO> CreateSectorDTOs()
        {
            return mapper.Map<List<Sector>, List<SectorDTO>>(CreateSectors());
        }

        public static List<Sector> CreateSectors()
        {
            return new List<Sector>()
            {
                new Sector
                {
                    Id = 1,
                    Number = 1,
                    Description = "Sector 1",
                    GpsLat = 49.112233M,
                    GpsLng = 23.223344M,
                    IsActive = false,
                    CreateDate = new DateTime(2019, 12, 28, 10, 20, 0),
                    CreateUserId = 1,
                    ModDate = new DateTime(2019, 12, 28, 10, 30, 0),
                    ModUserId = 2
                },
                new Sector
                {
                    Id = 2,
                    Number = 2,
                    Description = "Sector 2",
                    GpsLat = 49.986431M,
                    GpsLng = 23.331133M,
                    IsActive = false,
                    CreateDate = new DateTime(2019, 12, 29, 10, 20, 0),
                    CreateUserId = 2,
                    ModDate = new DateTime(2019, 12, 30, 10, 30, 0),
                    ModUserId = 1
                },
                new Sector
                {
                    Id = 3,
                    Number = 3,
                    Description = "Sector 3",
                    GpsLat = 49.472219M,
                    GpsLng = 23.098712M,
                    IsActive = true,
                    CreateDate = new DateTime(2020, 01, 01, 11, 22, 0),
                    CreateUserId = 1,
                    ModDate = new DateTime(2020, 01, 02, 10, 30, 0),
                    ModUserId = 2
                }
            };
        }

        public static SectorDTO CreateSectorDTO()
        {
            return new SectorDTO()
            {
                Id = 4,
                Number = 4,
                Description = "Sector 4",
                GpsLat = 49.472219M,
                GpsLng = 23.098712M,
                IsActive = true
            };
        }
    }
}
