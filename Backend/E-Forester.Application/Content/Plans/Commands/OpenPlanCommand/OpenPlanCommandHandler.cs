using E_Forester.Application.CustomExceptions;
using E_Forester.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Plans.Commands.OpenPlanCommand
{
    public class OpenPlanCommandHandler : IRequestHandler<OpenPlanCommand>
    {
        private readonly IPlanRepository _planRepository;

        public OpenPlanCommandHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Unit> Handle(OpenPlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanAsync(request.Id);

            if(plan == null)
                throw new NotFoundException();

            await _planRepository.OpenPlanAsync(plan);

            return await Task.FromResult(Unit.Value);
        }
    }
}
