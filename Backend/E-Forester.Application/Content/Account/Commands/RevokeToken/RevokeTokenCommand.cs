using MediatR;

namespace E_Forester.Application.Content.Account.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest
    {
        public string RefreshToken { get; set; }
    }
}
