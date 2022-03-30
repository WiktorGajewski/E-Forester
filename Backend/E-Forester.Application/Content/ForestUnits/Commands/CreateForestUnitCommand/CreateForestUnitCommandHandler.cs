using E_Forester.Application.CustomExceptions;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.ForestUnits.Commands.CreateForestUnitCommand
{
    public class CreateForestUnitCommandHandler : IRequestHandler<CreateForestUnitCommand>
    {
        private readonly IForestUnitRepository _forestUnitRepository;

        public CreateForestUnitCommandHandler(IForestUnitRepository forestUnitRepository)
        {
            _forestUnitRepository = forestUnitRepository;
        }

        public async Task<Unit> Handle(CreateForestUnitCommand request, CancellationToken cancellationToken)
        {
            var forestUnitsQuery = _forestUnitRepository.GetForestUnits();

            var checkDuplicate = forestUnitsQuery.FirstOrDefault(f => f.Address == request.Address);

            if (checkDuplicate != null)
                throw new BadRequestException("Leśnictwo o podanym adresie już istnieje");

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
