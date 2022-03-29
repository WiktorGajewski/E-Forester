using E_Forester.API.Attributes;
using E_Forester.Application.Content.Plans.Commands.ClosePlanCommand;
using E_Forester.Application.Content.Plans.Commands.CreatePlanCommand;
using E_Forester.Application.Content.Plans.Commands.OpenPlanCommand;
using E_Forester.Application.Content.Plans.Queries.GetPlanQuery;
using E_Forester.Application.Content.Plans.Queries.GetPlansQuery;
using E_Forester.Application.DataTransferObjects.Plans;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Model.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        [Produces("application/json")]
        [ProducesResponseType(typeof(Page<PlanDto>), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPlans([FromQuery] GetPlansQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PlanDto), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlan([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetPlanQuery() { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreatePlan([FromBody] CreatePlanCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [AuthorizedRole(new[] { UserRole.Admin })]
        [HttpPut("{id}/close")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ClosePlan([FromRoute] int id)
        {
            await _mediator.Send(new ClosePlanCommand() { Id = id });
            return NoContent();
        }

        [AuthorizedRole(new[] { UserRole.Admin })]
        [HttpPut("{id}/open")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> OpenPlan([FromRoute] int id)
        {
            await _mediator.Send(new OpenPlanCommand() { Id = id });
            return NoContent();
        }
    }
}
