using MediatR;

namespace E_Forester.Application.Content.Session.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest
    {
        public string RefreshToken { get; set; }
    }
}
