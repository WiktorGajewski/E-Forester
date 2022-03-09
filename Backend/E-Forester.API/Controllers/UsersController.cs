using E_Forester.Application.Content.Users.Commands.AssignForestUnitCommand;
using E_Forester.Application.Content.Users.Commands.UnassignForestUnitCommand;
using E_Forester.Application.Content.Users.Queries.GetUsersQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{userId}/forest-units/{forestUnitId}")]
        public async Task<IActionResult> AssignForestUnit([FromRoute] int userId, int forestUnitId) 
        {
            await _mediator.Send(new AssignForestUnitCommand() { UserId = userId, ForestUnitId = forestUnitId });
            return Ok();
        }

        [HttpDelete("{userId}/forest-units/{forestUnitId}")]
        public async Task<IActionResult> UnassignForestUnit([FromRoute] int userId, int forestUnitId)
        {
            await _mediator.Send(new UnassignForestUnitCommand() { UserId = userId, ForestUnitId = forestUnitId });
            return Ok();
        }
    }
}
