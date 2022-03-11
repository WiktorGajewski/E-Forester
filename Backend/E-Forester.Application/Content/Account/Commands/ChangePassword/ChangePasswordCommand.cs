using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Account.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest
    {
        [Required]
        [StringLength(70)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(70)]
        public string NewPassword { get; set; }
    }
}
