using E_Forester.Application.Content.Divisions.Commands.CreateDivisionCommand;
using E_Forester.Application.Content.Divisions.Queries.GetDivisionsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/divisions")]
    public class DivisionsController : BaseController
    {
        public DivisionsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetDivisions()
        {
            var result = await _mediator.Send(new GetDivisionsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDivision([FromBody] CreateDivisionCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
