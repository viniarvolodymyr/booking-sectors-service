using AttributeRouting.Web.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/bookings/")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingSectorService bookingService;

        public BookingController(IBookingSectorService bookingService, ISectorService sectorService)
        {
            this.bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dtos = await bookingService.GetBookingSectorsAsync();
            if (dtos.Any())
                return Ok(dtos);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dtos = await bookingService.GetBookingByIdAsync(id);
            return Ok(dtos);
        }

        [HttpPost]
        [HttpRoute("book")]
        public async Task<IActionResult> Post([FromBody]BookingSectorInfo bookingInfo)
        {
            await bookingService.BookSector(bookingInfo.SectorId, bookingInfo.From, bookingInfo.To, bookingInfo.UserId);
            return Ok();
        }

        [HttpPut("{id}")]
        public Task Put(int id, bool isApproved)
        {
            return bookingService.UpdateBookingApprovedAsync(id, isApproved); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await bookingService.DeleteBookingByIdAsync(id);
            return Ok();
        }
    }
}
