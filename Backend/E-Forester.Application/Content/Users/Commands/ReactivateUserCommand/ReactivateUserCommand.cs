using MediatR;

namespace E_Forester.Application.Content.Users.Commands.ReactivateUserCommand
{
    public class ReactivateUserCommand : IRequest
    {
        public int UserId { get; set; }
    }
}
