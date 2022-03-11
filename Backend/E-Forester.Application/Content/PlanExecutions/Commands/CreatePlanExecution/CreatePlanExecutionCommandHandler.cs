using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanExecutions.Commands.CreatePlanExecution
{
    public class CreatePlanExecutionCommandHandler : IRequestHandler<CreatePlanExecutionCommand>
    {
        private readonly IPlanExecutionRepository _planExecutionRepository;
        private readonly IPlanItemRepository _planItemRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IAuthService _authService;

        public CreatePlanExecutionCommandHandler(IPlanExecutionRepository planExecutionRepository, IPlanItemRepository planItemRepository, IPlanRepository planRepository, IAuthService authService)
        {
            _planExecutionRepository = planExecutionRepository;
            _planItemRepository = planItemRepository;
            _planRepository = planRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(CreatePlanExecutionCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanAsync(request.PlanId);
            var planItem = _planItemRepository.GetPlanItems().FirstOrDefault(p => p.Id == request.PlanItemId);

            if (plan == null)
                throw new BadRequestException("Plan not found");

            if (planItem == null)
                throw new BadRequestException("Plan item not found");

            if (!plan.PlanItems.Contains(planItem))
                throw new BadRequestException("Given plan item is not part of this plan");

            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var assignedForestUnits = await _authService.GetAssignedForestUnits();

                if (!assignedForestUnits.Contains(plan.ForestUnit))
                    throw new ForbiddenException();
            }

            var planExecution = new PlanExecution()
            {
                ExecutedHectares = request.ExecutedHectares,
                HarvestedCubicMeters = request.HarvestedCubicMeters,
                CreatedAt = DateTime.UtcNow,
                PlanItemId = request.PlanItemId,
                PlanId = request.PlanId,
                CreatorId = _authService.GetCurrentUserId()
            };

            await _planExecutionRepository.CreatePlanExecutionAsync(planExecution);

            return await Task.FromResult(Unit.Value);
        }
    }
}
