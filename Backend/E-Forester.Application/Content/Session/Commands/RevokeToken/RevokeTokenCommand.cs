using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Session.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
