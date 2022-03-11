using MediatR;

namespace E_Forester.Application.Content.Users.Commands.UnassignForestUnitCommand
{
    public class UnassignForestUnitCommand : IRequest
    {
        public int UserId { get; set; }
        public int ForestUnitId { get; set; }
    }
}
