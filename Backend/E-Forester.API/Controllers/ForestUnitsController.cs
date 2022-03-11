using E_Forester.Application.Content.ForestUnits.Commands.CreateForestUnitCommand;
using E_Forester.Application.Content.ForestUnits.Queries.GetForestUnitsQuery;
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

        [HttpPost]
        public async Task<IActionResult> CreateForestUnit([FromBody] CreateForestUnitCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
