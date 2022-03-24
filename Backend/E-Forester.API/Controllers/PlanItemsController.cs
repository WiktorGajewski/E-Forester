using E_Forester.API.Attributes;
using E_Forester.Application.Content.PlanItems.Commands.ClosePlanItemCommand;
using E_Forester.Application.Content.PlanItems.Commands.CreatePlanItemCommand;
using E_Forester.Application.Content.PlanItems.Commands.OpenPlanItemCommand;
using E_Forester.Application.Content.PlanItems.Queries.GetPlanItem;
using E_Forester.Application.Content.PlanItems.Queries.GetPlanItemsQuery;
using E_Forester.Application.DataTransferObjects.PlanItems;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Model.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        [Produces("application/json")]
        [ProducesResponseType(typeof(Page<PlanItemDto>), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPlanItems([FromQuery] GetPlanItemsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PlanItemDto), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlanItem([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetPlanItemQuery() { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreatePlanItem([FromBody] CreatePlanItemCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [AuthorizedRole(new[] { UserRole.Admin })]
        [HttpPut("close")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ClosePlanItems([FromBody] ClosePlanItemsCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [AuthorizedRole(new[] { UserRole.Admin })]
        [HttpPut("open")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> OpenPlanItems([FromBody] OpenPlanItemsCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
