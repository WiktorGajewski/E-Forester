using E_Forester.Application.DataTransferObjects.Account;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Infrastructure.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Session.Queries.RefreshToken
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, TokenDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenQueryHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<TokenDto> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
            var refreshToken = user?.RefreshTokens?.FirstOrDefault(t => t.Token == request.RefreshToken);

            if(user == null || refreshToken == null)
                throw new UnauthorizedAccessException("Invalid token");

            if (refreshToken.IsRevoked)
            {
                await _userRepository.RevokeAllRefreshTokens(user);
                throw new UnauthorizedAccessException("Invalid token");
            }

            if (refreshToken.IsExpired)
                throw new UnauthorizedAccessException("Token expired");

            if (user.IsActive == false)
                throw new UnauthorizedAccessException("Account is blocked");

            var newAccessToken = _tokenService.GenerateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var userRole = user.Role;

            await _userRepository.RevokeRefreshTokenAsync(refreshToken);
            await _userRepository.AddRefreshToken(newRefreshToken, user);

            await _userRepository.RemoveExpiredRefreshTokensAsync(user);

            return new TokenDto() { AccessToken = newAccessToken, RefreshToken = newRefreshToken.Token, UserRole = userRole };
        }
    }
}
