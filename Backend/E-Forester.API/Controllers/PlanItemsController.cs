using E_Forester.Application.Content.PlanItems.Commands.CreatePlanItemCommand;
using E_Forester.Application.Content.PlanItems.Queries.GetPlanItemsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/plan-items")]
    public class PlanItemsController : BaseController
    {
        public PlanItemsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetPlanItems([FromQuery] GetPlanItemsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlanItem([FromBody] CreatePlanItemCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
