using E_Forester.Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanItems.Commands.ClosePlanItemCommand
{
    public class ClosePlanItemsCommandHandler : IRequestHandler<ClosePlanItemsCommand>
    {
        private readonly IPlanItemRepository _planItemRepository;

        public ClosePlanItemsCommandHandler(IPlanItemRepository planItemRepository)
        {
            _planItemRepository = planItemRepository;
        }

        public async Task<Unit> Handle(ClosePlanItemsCommand request, CancellationToken cancellationToken)
        {
            await _planItemRepository.ClosePlanItemsAsync(request.planItemIds);

            return await Task.FromResult(Unit.Value);
        }
    }
}
