using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Users.Commands.ReactivateUserCommand
{
    public class ReactivateUserCommandHandler : IRequestHandler<ReactivateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public ReactivateUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(ReactivateUserCommand request, CancellationToken cancellationToken)
        {
            var auth = _authService.GetCurrentUserRole() == UserRole.Admin;

            if (!auth)
                throw new ForbiddenException();

            var user = await _userRepository.GetUserAsync(request.UserId);

            if (user == null)
                throw new NotFoundException("User not found");

            await _userRepository.ReactivateUserAsync(user);

            return await Task.FromResult(Unit.Value);
        }
    }
}
