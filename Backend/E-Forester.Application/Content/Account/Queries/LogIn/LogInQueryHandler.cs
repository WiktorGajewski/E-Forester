using E_Forester.Application.DataTransferObjects.Account;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Account.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, TokenDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginQueryHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<TokenDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var authenticated = await _userRepository.Authenticate(request.Login, request.Password);
            if (!authenticated)
                throw new UnauthorizedAccessException("Login failed.");

            var user = await _userRepository.GetUserAsync(request.Login);

            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _userRepository.AddRefreshToken(refreshToken, user);

            await _userRepository.RemoveExpiredRefreshTokensAsync(user);

            return new TokenDto() { AccessToken = token, RefreshToken = refreshToken.Token };
        }
    }
}
