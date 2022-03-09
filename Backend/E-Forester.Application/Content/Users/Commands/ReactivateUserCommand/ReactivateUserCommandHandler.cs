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
            this._userRepository = userRepository;
        }

        public async Task<Unit> Handle(ReactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.UserId);

            await _userRepository.ReactivateUserAsync(user);

            return await Task.FromResult(Unit.Value);
        }
    }
}
