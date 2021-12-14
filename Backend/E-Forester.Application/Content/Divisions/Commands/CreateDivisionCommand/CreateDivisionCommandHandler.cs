using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Divisions.Commands.CreateDivisionCommand
{
    public class CreateDivisionCommandHandler : IRequestHandler<CreateDivisionCommand>
    {
        private readonly IDivisionRepository _divisionRepository;

        public CreateDivisionCommandHandler(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public async Task<Unit> Handle(CreateDivisionCommand request, CancellationToken cancellationToken)
        {
            var division = new Division()
            {
                Address = request.Address,
                Area = request.Area,
                ForestUnitId = request.ForestUnitId
            };

            await _divisionRepository.CreateDivisionAsync(division);

            return await Task.FromResult(Unit.Value);
        }
    }
}
