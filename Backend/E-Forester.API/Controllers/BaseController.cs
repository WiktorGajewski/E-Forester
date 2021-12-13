using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Forester.API.Controllers
{
    [ApiController]
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
