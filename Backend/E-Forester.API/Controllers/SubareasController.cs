using E_Forester.Application.Content.Subareas.Commands.CreateSubareaCommand;
using E_Forester.Application.Content.Subareas.Queries.GetSubareasQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/subareas")]
    public class SubareasController : BaseController
    {
        public SubareasController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetSubareas([FromQuery] GetSubareasQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubarea([FromBody] CreateSubareaCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
