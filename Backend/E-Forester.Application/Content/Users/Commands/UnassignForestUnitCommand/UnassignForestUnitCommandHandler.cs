using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Users.Commands.UnassignForestUnitCommand
{
    public class UnassignForestUnitCommandHandler : IRequestHandler<UnassignForestUnitCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IForestUnitRepository _forestUnitRepository;
        private readonly IAuthService _authService;

        public UnassignForestUnitCommandHandler(IUserRepository userRepository, IForestUnitRepository forestUnitRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _forestUnitRepository = forestUnitRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(UnassignForestUnitCommand request, CancellationToken cancellationToken)
        {
            var auth = _authService.GetCurrentUserRole() == UserRole.Admin;

            if (!auth)
                throw new ForbiddenException();

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
