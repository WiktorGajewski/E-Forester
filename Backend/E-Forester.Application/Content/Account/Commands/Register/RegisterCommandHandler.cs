using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Account.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
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