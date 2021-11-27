using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Forester.API.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost("LogIn")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return Ok();
        }
    }
}
