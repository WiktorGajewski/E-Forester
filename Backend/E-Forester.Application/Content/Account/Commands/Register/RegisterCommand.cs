using E_Forester.Model.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Account.Commands.Register
{
    public class RegisterCommand : IRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(70)]
        public string Password { get; set; }

        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }
    }
}
