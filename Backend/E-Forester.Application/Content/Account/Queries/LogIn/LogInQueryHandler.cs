using E_Forester.Application.DataTransferObjects.Account;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Account.Queries.LogIn
{
    public class LogInQueryHandler : IRequestHandler<LogInQuery, TokenDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthHandler _authHandler;

        public LogInQueryHandler(IUserRepository userRepository, IAuthHandler authHandler)
        {
            _userRepository = userRepository;
            _authHandler = authHandler;
        }

        public async Task<TokenDto> Handle(LogInQuery request, CancellationToken cancellationToken)
        {
            var authenticated = await _userRepository.Authenticate(request.Login, request.Password);
            if (!authenticated)
                throw new UnauthorizedAccessException("Login failed.");

            var user = await _userRepository.GetUserAsync(request.Login);
            var token = _authHandler.GenerateToken(user);

            return new TokenDto() { Token = token };
        }
    }
}
