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

        [Authorize("Admin")]
        [HttpGet] 
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
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var dto = await bookingSectorService.GetBookingByIdAsync(id);
            if (dto != null)
            {
                return Ok(dto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("byUserId/{id}/{isActual}")]
        public async Task<IActionResult> GetByUserId([FromRoute]int id, [FromRoute]bool isActual)
        {
            var dtos = await bookingSectorService.GetBookingsByUserId(id, isActual);
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
        [Route("tournaments/{id}")]
        public async Task<IActionResult> GetTournament([FromRoute]int id)
        {
            var dtos = await bookingSectorService.GetBookingTournamentByIdAsync(id);
            if (dtos != null)
            {
                return Ok(dtos);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BookingSectorDTO bookingDTO)
        {
            var dto = await bookingSectorService.BookSector(bookingDTO);
            if (dto != null)
            {
                return Created($"api/bookings/{dto.Id}", dto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromQuery]bool? isApproved)
        {
            var booking = await bookingSectorService.UpdateBookingIsApprovedAsync(id, isApproved);
            if (booking != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("tournaments/{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] BookingSectorDTO bookingSectorDTO)
        {
            var bookingTournament = await bookingSectorService.UpdateBookingTournament(id, bookingSectorDTO);
            if (bookingTournament != null)
            {
                return Ok(bookingTournament);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var booking = await bookingSectorService.DeleteBookingByIdAsync(id);
            if (booking != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
