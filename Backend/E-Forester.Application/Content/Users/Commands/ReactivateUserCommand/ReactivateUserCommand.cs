using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Users.Commands.ReactivateUserCommand
{
    public class ReactivateUserCommand : IRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
