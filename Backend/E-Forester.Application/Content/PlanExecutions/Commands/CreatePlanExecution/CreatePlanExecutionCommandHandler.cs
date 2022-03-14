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
        private readonly IUserRepository _userRepository;

        public CreatePlanExecutionCommandHandler(IPlanExecutionRepository planExecutionRepository, IPlanItemRepository planItemRepository, IPlanRepository planRepository, IAuthService authService, IUserRepository userRepository)
        {
            _planExecutionRepository = planExecutionRepository;
            _planItemRepository = planItemRepository;
            _planRepository = planRepository;
            _authService = authService;
            _userRepository = userRepository;
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

            if (await CheckAssignedForestUnit(plan.ForestUnit))
                throw new ForbiddenException();

            if (planItem.IsCompleted)
                throw new BadRequestException("Plan item is already completed");

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

        private async Task<bool> CheckAssignedForestUnit(ForestUnit checkForestUnit)
        {
            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var id = _authService.GetCurrentUserId();
                var user = await _userRepository.GetUserAsync(id);

                if (!user.AssignedForestUnits.Contains(checkForestUnit))
                    return false;
            }

            return true;
        }
    }
}
