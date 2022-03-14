using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Users.Commands.DeactivateUserCommand
{
    public class DeactivateUserCommand : IRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
