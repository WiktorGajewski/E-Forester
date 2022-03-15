using E_Forester.Application.CustomExceptions;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Subareas.Commands.CreateSubareaCommand
{
    public class CreateSubareaCommandHandler : IRequestHandler<CreateSubareaCommand>
    {
        private readonly ISubareaRepository _subareaRepository;
        private readonly IDivisionRepository _divisionRepository;

        public CreateSubareaCommandHandler(ISubareaRepository subareaRepository, IDivisionRepository divisionRepository)
        {
            _subareaRepository = subareaRepository;
            _divisionRepository = divisionRepository;
        }

        public async Task<Unit> Handle(CreateSubareaCommand request, CancellationToken cancellationToken)
        {
            var division = _divisionRepository.GetDivisions().FirstOrDefault(d => d.Id == request.DivisionId);

            if(division == null)
                throw new BadRequestException("Nie znaleziono oddziału o podanym Id");

            var subarea = new Subarea()
            {
                Address = request.Address,
                Area = request.Area,
                DivisionId = request.DivisionId
            };

            await _subareaRepository.AddSubareaAsync(subarea);

            return await Task.FromResult(Unit.Value);
        }
    }
}
