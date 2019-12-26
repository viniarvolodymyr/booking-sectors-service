using Microsoft.AspNetCore.Authorization;
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
        private readonly IBookingSectorService bookingSectorService;

        public BookingSectorController(IBookingSectorService bookingSectorService)
        {
            this.bookingSectorService = bookingSectorService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var dtos = await bookingSectorService.GetBookingSectorsAsync();
            if (dtos.Any())
            {
                return Ok(dtos);
            }
            else
            {
                return NotFound();
            }
        }

        
        [HttpGet]
        [Route("tournaments")]
        public async Task<IActionResult> GetTournaments()
        {
            var dtos = await bookingSectorService.GetBookingTournamentSectorsAsync();
            
            if (dtos.Any())
            {
                return Ok(dtos);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var dto = await bookingSectorService.GetBookingByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dto);
            }
        }
        [HttpGet]
        [Route("tournaments/{id}")]
        public async Task<IActionResult> GetTournament([FromRoute]int id)
        {
            var dto = await bookingSectorService.GetBookingTournamentByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dto);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Post([FromBody]BookingSectorDTO bookingDTO)
        {
            var dto = await bookingSectorService.BookSector(bookingDTO);
            if (dto == null)
            {
                return BadRequest();
            }
            else
            {
                return Created($"api/bookings/{dto.Id}", dto);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromQuery]bool isApproved)
        {
            var booking = await bookingSectorService.UpdateBookingApprovedAsync(id, isApproved);
            if (booking == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(booking);
            }
        }
        [HttpPut]
        [Route("tournaments/{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] BookingSectorDTO bookingSectorDTO)
        {
            var bookedTournament = await bookingSectorService.UpdateTournamentBooking(id, bookingSectorDTO);
            if (bookedTournament == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(bookedTournament);
            }
        }



        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var booking = await bookingSectorService.DeleteBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(booking);
            }
        }
    }
}
