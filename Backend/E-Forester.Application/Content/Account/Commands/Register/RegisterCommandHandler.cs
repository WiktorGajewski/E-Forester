using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Account.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var auth = _authService.GetCurrentUserRole() == UserRole.Admin;

            if(!auth)
                throw new ForbiddenException();

            var checkLogin = await _userRepository.GetUserAsync(request.Login);

            if (checkLogin != null)
                throw new BadRequestException("User with such login already exists");

            var newUser = new User()
            {
                Name = request.Name,
                Login = request.Login,
                Password = request.Password,
                Role = request.Role,
                RegistrationDate = DateTime.UtcNow,
                IsActive = true
            };

            await _userRepository.RegisterUserAsync(newUser);

            return await Task.FromResult(Unit.Value);
        }
    }
}