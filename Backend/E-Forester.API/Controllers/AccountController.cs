using E_Forester.API.Attributes;
using E_Forester.Application.Content.Account.Commands.ChangePassword;
using E_Forester.Application.Content.Account.Commands.Register;
using E_Forester.Application.Content.Account.Queries.GetProfileInfo;
using E_Forester.Application.Content.Account.Queries.Login;
using E_Forester.Application.DataTransferObjects.Account;
using E_Forester.Application.DataTransferObjects.Users;
using E_Forester.Model.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly IConfiguration _configuration;

        public AccountController(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProfileInfo()
        {
            var result = await _mediator.Send(new GetProfileInfoQuery());
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            var result = await _mediator.Send(query);
            setTokenCookie(result.RefreshToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpOptions("login")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult LoginOptions()
        {
            return NoContent();
        }

        [AuthorizedRole(new[] { UserRole.Admin })]
        [HttpPost("register")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("change-password")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            await _mediator.Send(command);
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
                SameSite = SameSiteMode.None,
                MaxAge = TimeSpan.FromHours(durationTime)
            };

            Response.Cookies.Append("RefreshToken", token, cookieOptions);
        }
    }
}
