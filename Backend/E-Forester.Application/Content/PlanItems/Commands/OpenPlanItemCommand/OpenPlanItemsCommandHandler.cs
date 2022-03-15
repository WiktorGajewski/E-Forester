using E_Forester.Data.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanItems.Commands.OpenPlanItemCommand
{
    public class OpenPlanItemsCommandHandler : IRequestHandler<OpenPlanItemsCommand>
    {
        private readonly IPlanItemRepository _planItemRepository;

        public OpenPlanItemsCommandHandler(IPlanItemRepository planItemRepository)
        {
            _planItemRepository = planItemRepository;
        }

        public async Task<Unit> Handle(OpenPlanItemsCommand request, CancellationToken cancellationToken)
        {
            await _planItemRepository.OpenPlanItemsAsync(request.planItemIds);

            return await Task.FromResult(Unit.Value);
        }
    }
}
