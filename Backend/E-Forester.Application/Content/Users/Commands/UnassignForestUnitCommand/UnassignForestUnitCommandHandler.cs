using E_Forester.Application.CustomExceptions;
using E_Forester.Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Users.Commands.UnassignForestUnitCommand
{
    public class UnassignForestUnitCommandHandler : IRequestHandler<UnassignForestUnitCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IForestUnitRepository _forestUnitRepository;

        public UnassignForestUnitCommandHandler(IUserRepository userRepository, IForestUnitRepository forestUnitRepository)
        {
            _userRepository = userRepository;
            _forestUnitRepository = forestUnitRepository;
        }

        public async Task<Unit> Handle(UnassignForestUnitCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.UserId);
            var forestUnit = await _forestUnitRepository.GetForestUnitAsync(request.ForestUnitId);

            if (user == null)
                throw new NotFoundException("User not found");

            if (forestUnit == null)
                throw new NotFoundException("Forest unit not found");

            await _userRepository.UnassignForestUnitAsync(user, forestUnit);

            return await Task.FromResult(Unit.Value);
        }
    }
}
