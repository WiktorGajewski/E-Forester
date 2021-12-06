using E_Forester.Application.Content.Account.Commands.Register;
using E_Forester.Application.Content.Account.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IConfiguration _configuration;

        public AccountController(IMediator mediator, IConfiguration configuration) : base(mediator)
        {
            _configuration = configuration;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            var result = await _mediator.Send(query);
            setTokenCookie(result.RefreshToken);
            return Ok(result);
        }

        [HttpOptions("Login")]
        [AllowAnonymous]
        public IActionResult LoginOptions()
        {
            Response.Headers.Add("Allow", "POST,OPTIONS");
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            await _mediator.Send(command);
            return Ok();
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
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
