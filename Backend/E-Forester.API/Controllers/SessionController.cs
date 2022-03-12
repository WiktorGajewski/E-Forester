﻿using E_Forester.Application.Content.Session.Commands.RevokeToken;
using E_Forester.Application.Content.Session.Queries.RefreshToken;
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
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];

            var result = await _mediator.Send(new RefreshTokenQuery() { RefreshToken = refreshToken });

            setTokenCookie(result.RefreshToken);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpOptions("refresh-token")]
        public IActionResult RefreshTokenOptions()
        {
            return NoContent();
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];

            if (refreshToken == null)
                return BadRequest(new { Error = "Token is required" });

            await _mediator.Send(new RevokeTokenCommand() { RefreshToken = refreshToken });

            return Ok();
        }

        [HttpOptions("revoke-token")]
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
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromHours(durationTime)
            };

            Response.Cookies.Append("RefreshToken", token, cookieOptions);
        }
    }
}
