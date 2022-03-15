using E_Forester.Infrastructure.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Session.Commands.RevokeToken
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly IUserRepository _userRepository;

        public RevokeTokenCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
            var refreshToken = user?.RefreshTokens?.FirstOrDefault(t => t.Token == request.RefreshToken);

            if (user == null || refreshToken == null || refreshToken.IsActive == false)
                throw new UnauthorizedAccessException("Invalid token");

            await _userRepository.RevokeRefreshTokenAsync(refreshToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
