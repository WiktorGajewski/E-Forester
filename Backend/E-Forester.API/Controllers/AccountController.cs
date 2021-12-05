using E_Forester.Application.Content.Account.Commands.Register;
using E_Forester.Application.Content.Account.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            var result = await _mediator.Send(query);
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
    }
}
