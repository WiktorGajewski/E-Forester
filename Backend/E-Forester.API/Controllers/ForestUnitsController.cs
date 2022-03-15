using E_Forester.API.Attributes;
using E_Forester.Application.Content.ForestUnits.Commands.CreateForestUnitCommand;
using E_Forester.Application.Content.ForestUnits.Queries.GetForestUnitsQuery;
using E_Forester.Model.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/forest-units")]
    public class ForestUnitsController : BaseController
    {
        public ForestUnitsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetForestUnits([FromQuery] GetForestUnitsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [AuthorizedRole(new[] { UserRole.Admin })]
        [HttpPost]
        public async Task<IActionResult> CreateForestUnit([FromBody] CreateForestUnitCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
