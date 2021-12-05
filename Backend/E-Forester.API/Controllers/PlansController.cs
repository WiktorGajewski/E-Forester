using E_Forester.Application.Content.Plans.Commands.CreatePlanCommand;
using E_Forester.Application.Content.Plans.Queries.GetPlansQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    public class PlansController : BaseController
    {
        public PlansController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetPlans()
        {
            var result = await _mediator.Send(new GetPlansQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] CreatePlanCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
