using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;

namespace SoftServe.BookingSectors.WebAPI.Controllers
{
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
        public async Task<ActionResult<TokenDTO>> SignIn([FromBody]SignInDTO credentials )
        {
            var result = await authenticationService.SignInAsync(credentials.Phone, credentials.Password);
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
