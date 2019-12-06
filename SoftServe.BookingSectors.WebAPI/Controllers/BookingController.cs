using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.Interfaces;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IBookingSectorService _bookingService;
        public BookingController(IBookingSectorService bookingService)
        {
            _bookingService = bookingService;
                
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingSectorDTO>>> Get()
        {
            var dtos = await _bookingService.GetBookingSectorsAsync();
            if(!dtos.Any())
            {
                return NoContent();
            }
            return Ok(dtos);
        }

        // GET: api/Booking/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<BookingSectorDTO>> Get(int id)
        {
            var dtos = await _bookingService.GetBookingByIdAsync(id);
            if(dtos == null)
            {
                return NotFound();
            }
            return Ok(dtos);
        }

        // POST: api/Booking
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Booking/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
