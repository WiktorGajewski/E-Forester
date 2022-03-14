using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Divisions.Commands.CreateDivisionCommand
{
    public class CreateDivisionCommandHandler : IRequestHandler<CreateDivisionCommand>
    {
        private readonly IDivisionRepository _divisionRepository;
        private readonly IAuthService _authService;

        public CreateDivisionCommandHandler(IDivisionRepository divisionRepository, IAuthService authService)
        {
            _divisionRepository = divisionRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(CreateDivisionCommand request, CancellationToken cancellationToken)
        {
            var auth = _authService.GetCurrentUserRole() == UserRole.Admin;

            if (!auth)
                throw new ForbiddenException();

            var division = new Division()
            {
                Address = request.Address,
                Area = request.Area,
                ForestUnitId = request.ForestUnitId
            };

            await _divisionRepository.AddDivisionAsync(division);

            return await Task.FromResult(Unit.Value);
        }
    }
}
