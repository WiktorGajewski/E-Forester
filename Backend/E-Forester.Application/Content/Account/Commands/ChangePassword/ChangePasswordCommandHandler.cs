using E_Forester.Application.Security.Interfaces;
using E_Forester.Infrastructure.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Account.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public ChangePasswordCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _authService.GetCurrentUserId();
            var user = await _userRepository.GetUserAsync(userId);

            var authenticated = await _userRepository.Authenticate(user.Login, request.OldPassword);
            if (!authenticated)
                throw new UnauthorizedAccessException("Uwierzytelnianie nie powiodło się");

            await _userRepository.ChangePasswordAsync(user, request.NewPassword);

            return await Task.FromResult(Unit.Value);
        }
    }
}

