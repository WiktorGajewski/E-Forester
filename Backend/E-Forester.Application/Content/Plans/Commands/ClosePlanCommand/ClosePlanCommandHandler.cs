using E_Forester.Application.CustomExceptions;
using E_Forester.Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Plans.Commands.ClosePlanCommand
{
    public class ClosePlanCommandHandler : IRequestHandler<ClosePlanCommand>
    {
        private readonly IPlanRepository _planRepository;

        public ClosePlanCommandHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Unit> Handle(ClosePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanAsync(request.Id);

            if (plan == null)
                throw new NotFoundException("Nie znaleziono planu o podanym Id");

            await _planRepository.ClosePlanAsync(plan);

            return await Task.FromResult(Unit.Value);
        }
    }
}
