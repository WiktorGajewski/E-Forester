using E_Forester.Application.DataTransferObjects.Account;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Session.Queries.RefreshToken
{
    public class RefreshTokenQuery : IRequest<TokenDto>
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
