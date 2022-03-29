using E_Forester.API.Attributes;
using E_Forester.Application.Content.Users.Commands.AssignForestUnitCommand;
using E_Forester.Application.Content.Users.Commands.DeactivateUserCommand;
using E_Forester.Application.Content.Users.Commands.ReactivateUserCommand;
using E_Forester.Application.Content.Users.Commands.UnassignForestUnitCommand;
using E_Forester.Application.Content.Users.Queries.GetUsersQuery;
using E_Forester.Application.DataTransferObjects.Users;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Model.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Forester.API.Controllers
{
    [Route("api/users")]
    [AuthorizedRole(new[] { UserRole.Admin })]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Page<UserDto>), 200)]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{userId}/reactivate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReactivateUser([FromRoute] int userId)
        {
            await _mediator.Send(new ReactivateUserCommand() { UserId = userId });
            return NoContent();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeactivateUser([FromRoute] int userId)
        {
            await _mediator.Send(new DeactivateUserCommand() { UserId = userId });
            return NoContent();
        }

        [HttpPut("{userId}/forest-units/{forestUnitId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignForestUnit([FromRoute] int userId, int forestUnitId) 
        {
            await _mediator.Send(new AssignForestUnitCommand() { UserId = userId, ForestUnitId = forestUnitId });
            return NoContent();
        }

        [HttpDelete("{userId}/forest-units/{forestUnitId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnassignForestUnit([FromRoute] int userId, int forestUnitId)
        {
            await _mediator.Send(new UnassignForestUnitCommand() { UserId = userId, ForestUnitId = forestUnitId });
            return NoContent();
        }
    }
}
