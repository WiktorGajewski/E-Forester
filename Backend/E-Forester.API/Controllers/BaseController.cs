using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_Forester.API.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
