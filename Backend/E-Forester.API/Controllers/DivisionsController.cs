using E_Forester.API.Attributes;
using E_Forester.Application.Content.Divisions.Commands.CreateDivisionCommand;
using E_Forester.Application.Content.Divisions.Queries.GetDivisionsQuery;
using E_Forester.Model.Enums;
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
        public async Task<IActionResult> GetDivisions([FromQuery] GetDivisionsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [AuthorizedRole(new[] { UserRole.Admin })]
        [HttpPost]
        public async Task<IActionResult> CreateDivision([FromBody] CreateDivisionCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
