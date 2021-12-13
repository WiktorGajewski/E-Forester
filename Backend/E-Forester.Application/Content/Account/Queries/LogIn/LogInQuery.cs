using E_Forester.Application.DataTransferObjects.Account;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Account.Queries.Login
{
    public class LoginQuery : IRequest<TokenDto>
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
