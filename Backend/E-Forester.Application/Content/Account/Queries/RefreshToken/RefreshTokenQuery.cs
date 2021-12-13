using E_Forester.Application.DataTransferObjects.Account;
using MediatR;

namespace E_Forester.Application.Content.Account.Queries.RefreshToken
{
    public class RefreshTokenQuery : IRequest<TokenDto>
    {
        public string RefreshToken { get; set; }
    }
}
