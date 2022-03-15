using E_Forester.Application.CustomExceptions;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Divisions.Commands.CreateDivisionCommand
{
    public class CreateDivisionCommandHandler : IRequestHandler<CreateDivisionCommand>
    {
        private readonly IDivisionRepository _divisionRepository;
        private readonly IForestUnitRepository _forestUnitRepository;

        public CreateDivisionCommandHandler(IDivisionRepository divisionRepository, IForestUnitRepository forestUnitRepository)
        {
            _divisionRepository = divisionRepository;
            _forestUnitRepository = forestUnitRepository;
        }

        public async Task<Unit> Handle(CreateDivisionCommand request, CancellationToken cancellationToken)
        {
            var forestUnit = _forestUnitRepository.GetForestUnits().FirstOrDefault(f => f.Id == request.ForestUnitId);

            if (forestUnit == null)
                throw new BadRequestException("Nie znaleziono leśnictwa o podanym Id");

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
