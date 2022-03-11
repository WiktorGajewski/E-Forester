using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanItems.Commands.ClosePlanItemCommand
{
    public class ClosePlanItemsCommandHandler : IRequestHandler<ClosePlanItemsCommand>
    {
        private readonly IPlanItemRepository _planItemRepository;
        private readonly IAuthService _authService;

        public ClosePlanItemsCommandHandler(IPlanItemRepository planItemRepository, IAuthService authService)
        {
            _planItemRepository = planItemRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(ClosePlanItemsCommand request, CancellationToken cancellationToken)
        {
            var auth = _authService.GetCurrentUserRole() == UserRole.Admin;

            if (!auth)
                throw new ForbiddenException();

            await _planItemRepository.ClosePlanItemsAsync(request.planItemIds);

            return await Task.FromResult(Unit.Value);
        }
    }
}
