using E_Forester.Application.CustomExceptions;
using E_Forester.Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Users.Commands.DeactivateUserCommand
{
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeactivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.UserId);

            if (user == null)
                throw new NotFoundException("Nie znaleziono użytkownika o podanym Id");

            await _userRepository.DeactivateUserAsync(user);

            return await Task.FromResult(Unit.Value);
        }
    }
}
