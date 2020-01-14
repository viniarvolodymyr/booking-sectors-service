using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using System.Collections.Generic;

namespace SoftServe.BookingSectors.WebAPI.Tests.ControllersTests.Data
{
    public class SectorData
    {
        public SectorData() { }
        public List<SectorDTO> Sectors { get; } = new List<SectorDTO>()
        {
            new SectorDTO
            {
                Id = 1,
                Number = 1,
                Description = "Sector 1",
                GpsLat = 49.112233M,
                GpsLng = 23.223344M,
                IsActive = false
            },
            new SectorDTO
            {
                Id = 2,
                Number = 2,
                Description = "Sector 2",
                GpsLat = 49.986431M,
                GpsLng = 23.331133M,
                IsActive = false
            },
            new SectorDTO
            {
                Id = 3,
                Number = 3,
                Description = "Sector 3",
                GpsLat = 49.472219M,
                GpsLng = 23.098712M,
                IsActive = true
            }
        };
        public SectorDTO SectorDTOToInsert { get; } = new SectorDTO()
        {
            Id = 4,
            Number = 4,
            Description = "Sector 4",
            GpsLat = 49.472219M,
            GpsLng = 23.098712M,
            IsActive = true
        };
        public List<SectorDTO> FreeSectors { get; } = new List<SectorDTO>()
        {
            new SectorDTO
            {
                Id = 3,
                Number = 3,
                Description = "Sector 3",
                GpsLat = 49.472219M,
                GpsLng = 23.098712M,
                IsActive = true
            }
        };
    }
}
