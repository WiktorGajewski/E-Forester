using E_Forester.Application.Content.PlanExecutions.Commands.CreatePlanExecution;
using E_Forester.Application.Content.PlanExecutions.Queries.GetPlanExecutions;
using E_Forester.Application.DataTransferObjects.PlanExecutions;
using E_Forester.Application.Pagination.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        [Produces("application/json")]
        [ProducesResponseType(typeof(Page<PlanExecutionDto>), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPlanExecutions([FromQuery] GetPlanExecutionsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreatePlanExecution([FromBody] CreatePlanExecutionCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
