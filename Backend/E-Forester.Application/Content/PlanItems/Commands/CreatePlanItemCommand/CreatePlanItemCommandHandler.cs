using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanItems.Commands.CreatePlanItemCommand
{
    public class CreatePlanItemCommandHandler : IRequestHandler<CreatePlanItemCommand>
    {
        private readonly IPlanItemRepository _planItemRepository;
        private readonly IPlanRepository _planRepository;
        private readonly ISubareaRepository _subareaRepository;
        private readonly IAuthService _authService;

        public CreatePlanItemCommandHandler(IPlanItemRepository planItemRepository, IPlanRepository planRepository, ISubareaRepository subareaRepository, IAuthService authService)
        {
            _planItemRepository = planItemRepository;
            _planRepository = planRepository;
            _subareaRepository = subareaRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(CreatePlanItemCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanAsync(request.PlanId);
            var subarea = await _subareaRepository.GetSubareaAsync(request.SubareaId);

            if(plan == null)
                throw new BadRequestException("Plan not found");

            if (subarea == null)
                throw new BadRequestException("Subarea not found");

            if (subarea.Division.Id == plan.ForestUnitId)
                throw new BadRequestException("Plan and subarea belong to two different forest units");

            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var assignedForestUnits = await _authService.GetAssignedForestUnits();

                if (!assignedForestUnits.Contains(plan.ForestUnit))
                    throw new ForbiddenException();
            }

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