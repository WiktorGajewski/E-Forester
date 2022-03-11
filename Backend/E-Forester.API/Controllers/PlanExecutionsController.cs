using E_Forester.Application.Content.PlanExecutions.Commands.CreatePlanExecution;
using E_Forester.Application.Content.PlanExecutions.Queries.GetPlanExecutions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/plan-executions")]
    public class PlanExecutionsController : BaseController
    {
        public PlanExecutionsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetPlanExecutions([FromQuery] GetPlanExecutionsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlanExecution([FromBody] CreatePlanExecutionCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
