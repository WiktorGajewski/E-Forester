using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanItems.Commands.CreatePlanItemCommand
{
    public class CreatePlanItemCommandHandler : IRequestHandler<CreatePlanItemCommand>
    {
        private readonly IPlanItemRepository _planItemRepository;
        private readonly IAuthService _authService;

        public CreatePlanItemCommandHandler(IPlanItemRepository planItemRepository, IAuthService authService)
        {
            _planItemRepository = planItemRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(CreatePlanItemCommand request, CancellationToken cancellationToken)
        {
            var planItem = new PlanItem()
            {
                IsCompleted = false,
                PlannedCubicMeters = request.PlannedCubicMeters,
                PlannedHectares = request.PlannedHectares,
                Assortments = request.Assortments,
                ActionGroup = request.ActionGroup,
                DifficultyLevel = request.DifficultyLevel,
                Factor = request.Factor,
                CreatedAt = DateTime.UtcNow,
                PlanId = request.PlanId,
                SubareaId = request.SubareaId,
                CreatorId = _authService.GetCurrentUserId()
            };

            await _planItemRepository.CreatePlanItemAsync(planItem);

            return await Task.FromResult(Unit.Value);
        }
    }
}