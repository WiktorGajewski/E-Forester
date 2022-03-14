using MediatR;

namespace E_Forester.Application.Content.Users.Commands.DeactivateUserCommand
{
    public class DeactivateUserCommand : IRequest
    {
        public int UserId { get; set; }
    }
}
