using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.ForestUnits.Commands.CreateForestUnitCommand
{
    public class CreateForestUnitCommandHandler : IRequestHandler<CreateForestUnitCommand>
    {
        private readonly IForestUnitRepository _forestUnitRepository;
        private readonly IAuthService _authService;

        public CreateForestUnitCommandHandler(IForestUnitRepository forestUnitRepository, IAuthService authService)
        {
            _forestUnitRepository = forestUnitRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(CreateForestUnitCommand request, CancellationToken cancellationToken)
        {
            var auth = _authService.GetCurrentUserRole() == UserRole.Admin;

            if (!auth)
                throw new ForbiddenException();

            var forestUnit = new ForestUnit()
            {
                Name = request.Name,
                Address = request.Address,
                Area = request.Area
            };

            await _forestUnitRepository.AddForestUnitAsync(forestUnit);

            return await Task.FromResult(Unit.Value);
        }
    }
}
