using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using AttributeRouting.Web.Http;


namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/bookings/")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingSectorService bookingService;
        private ISectorService sectorService;

        public BookingController(IBookingSectorService bookingService, ISectorService sectorService)
        {
            this.bookingService = bookingService;
            this.sectorService = sectorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingSectorDTO>>> Get()
        {
            var dtos = await bookingService.GetBookingSectorsAsync();
            if (!dtos.Any())
            {
                return NoContent();
            }
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<BookingSectorDTO>> Get(int id)
        {
            var dtos = await bookingService.GetBookingByIdAsync(id);
            if (dtos == null)
            {
                return NotFound();
            }
            return Ok(dtos);
        }

        // POST: api/Booking
        [HttpPost]
        public void Post([FromBody] int sectorNumber, DateTime fromDate, DateTime toDate, int userId)
        {


        }

        // PUT: api/Booking/5
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] bool isApproved)
        {
            return bookingService.UpdateBookingApprovedAsync(id, isApproved); 
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await bookingService.DeleteBookingByIdAsync(id);
            return Ok();
        }
    }
}
