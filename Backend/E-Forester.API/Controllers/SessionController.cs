using E_Forester.Application.Content.Session.Commands.RevokeToken;
using E_Forester.Application.Content.Session.Queries.RefreshToken;
using E_Forester.Application.DataTransferObjects.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/session")]
    public class SessionController : BaseController
    {
        private readonly IConfiguration _configuration;

        public SessionController(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];

            var result = await _mediator.Send(new RefreshTokenQuery() { RefreshToken = refreshToken });

            setTokenCookie(result.RefreshToken);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpOptions("refresh-token")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult RefreshTokenOptions()
        {
            return NoContent();
        }

        [HttpPost("revoke-token")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RevokeToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];

            if (refreshToken == null)
                return BadRequest(new { Error = "Token is required" });

            await _mediator.Send(new RevokeTokenCommand() { RefreshToken = refreshToken });

            return NoContent();
        }

        [HttpOptions("revoke-token")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult RevokeTokenOptions()
        {
            return NoContent();
        }

        private void setTokenCookie(string token)
        {
            var durationTime = Convert.ToInt32(_configuration["RefreshToken:DurationInHours"]);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(durationTime),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
                MaxAge = TimeSpan.FromHours(durationTime)
            };

            Response.Cookies.Append("RefreshToken", token, cookieOptions);
        }
    }
}
