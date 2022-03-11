using MediatR;

namespace E_Forester.Application.Content.Users.Commands.AssignForestUnitCommand
{
    public class AssignForestUnitCommand : IRequest
    {
        public int UserId { get; set; }
        public int ForestUnitId { get; set; }
    }
}
