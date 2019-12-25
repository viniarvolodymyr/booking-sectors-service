using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebApi.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
    [Authorize]
    [Route("api/authentication")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<TokenDTO>> SignIn(string phone, string password)
        {
            var result = await authenticationService.SignInAsync(phone, password);
            if (result == null)
                return BadRequest();
            
            return result;
        }

        [HttpPost("refresh_token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDTO token)
        {
            var result = await authenticationService.TokenAsync(token);
            return result != null
                ? (IActionResult)result
                : Forbid();
        }
    }
}
