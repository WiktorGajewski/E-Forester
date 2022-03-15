using E_Forester.Application.CustomExceptions;
using E_Forester.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Users.Commands.ReactivateUserCommand
{
    public class ReactivateUserCommandHandler : IRequestHandler<ReactivateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public ReactivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ReactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.UserId);

            if (user == null)
                throw new NotFoundException("User not found");

            await _userRepository.ReactivateUserAsync(user);

            return await Task.FromResult(Unit.Value);
        }
    }
}
