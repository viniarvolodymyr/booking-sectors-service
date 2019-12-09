using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using AttributeRouting.Web.Http;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/bookings/")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingSectorService bookingService;
        private readonly ISectorService sectorService;

        public BookingController(IBookingSectorService bookingService, ISectorService sectorService)
        {
            this.bookingService = bookingService;
            this.sectorService = sectorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dtos = await bookingService.GetBookingSectorsAsync();
            if (dtos.Any())
            {
                return Ok(dtos);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dtos = await bookingService.GetBookingByIdAsync(id);
            if (dtos == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dtos);
            }
        }

        // POST: api/Booking
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BookingSectorInfo bookingInfo)
        {
            await bookingService.BookSector(bookingInfo.SectorId, bookingInfo.From, bookingInfo.To, bookingInfo.UserId);
            return Ok();
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
