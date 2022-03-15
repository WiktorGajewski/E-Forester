using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Subareas.Commands.CreateSubareaCommand
{
    public class CreateSubareaCommandHandler : IRequestHandler<CreateSubareaCommand>
    {
        private readonly ISubareaRepository _subareaRepository;

        public CreateSubareaCommandHandler(ISubareaRepository subareaRepository)
        {
            _subareaRepository = subareaRepository;
        }

        public async Task<Unit> Handle(CreateSubareaCommand request, CancellationToken cancellationToken)
        {
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
