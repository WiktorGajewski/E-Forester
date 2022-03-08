using E_Forester.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Users.Commands.AssignForestUnitCommand
{
    public class AssignForestUnitCommandHandler : IRequestHandler<AssignForestUnitCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IForestUnitRepository _forestUnitRepository;

        public AssignForestUnitCommandHandler(IUserRepository userRepository, IForestUnitRepository forestUnitRepository)
        {
            this._userRepository = userRepository;
            this._forestUnitRepository = forestUnitRepository;
        }

        public async Task<Unit> Handle(AssignForestUnitCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.UserId);
            var forestUnit = await _forestUnitRepository.GetForestUnitAsync(request.ForestUnitId);

            await _userRepository.AssignForestUnitAsync(user, forestUnit);

            return await Task.FromResult(Unit.Value);
        }
    }
}

