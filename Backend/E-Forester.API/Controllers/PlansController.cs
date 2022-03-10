using E_Forester.Application.Content.Plans.Commands.ClosePlanCommand;
using E_Forester.Application.Content.Plans.Commands.CreatePlanCommand;
using E_Forester.Application.Content.Plans.Commands.OpenPlanCommand;
using E_Forester.Application.Content.Plans.Queries.GetPlanQuery;
using E_Forester.Application.Content.Plans.Queries.GetPlansQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/plans")]
    public class PlansController : BaseController
    {
        public PlansController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetPlans([FromQuery] GetPlansQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlan([FromRoute] int Id)
        {
            var result = await _mediator.Send(new GetPlanQuery() { Id = Id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] CreatePlanCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}/close")]
        public async Task<IActionResult> UpdatePlan([FromRoute] int id)
        {
            await _mediator.Send(new ClosePlanCommand() { Id = id });
            return NoContent();
        }

        [HttpPut("{id}/open")]
        public async Task<IActionResult> OpenPlan([FromRoute] int id)
        {
            await _mediator.Send(new OpenPlanCommand() { Id = id });
            return NoContent();
        }
    }
}
