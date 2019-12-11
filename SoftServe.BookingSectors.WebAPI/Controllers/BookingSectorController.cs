using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingSectorController : ControllerBase
    {
        private readonly IBookingSectorService bookingService;

        public BookingSectorController(IBookingSectorService bookingService)
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
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var dtos = await bookingService.GetBookingByIdAsync(id);
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BookingSectorDTO bookingDTO)
        {
            await bookingService.BookSector(bookingDTO);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromQuery]bool isApproved)
        {
            var booking = await bookingService.UpdateBookingApprovedAsync(id, isApproved);
            if (booking == null)
                return NotFound();
            return Ok(booking);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await bookingService.DeleteBookingByIdAsync(id);
            return Ok();
        }
    }
}
