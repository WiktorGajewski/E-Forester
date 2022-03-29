using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanItems.Commands.CreatePlanItemCommand
{
    public class CreatePlanItemCommandHandler : IRequestHandler<CreatePlanItemCommand>
    {
        private readonly IPlanItemRepository _planItemRepository;
        private readonly IPlanRepository _planRepository;
        private readonly ISubareaRepository _subareaRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public CreatePlanItemCommandHandler(IPlanItemRepository planItemRepository, IPlanRepository planRepository, ISubareaRepository subareaRepository, IAuthService authService, IUserRepository userRepository)
        {
            _planItemRepository = planItemRepository;
            _planRepository = planRepository;
            _subareaRepository = subareaRepository;
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(CreatePlanItemCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanAsync(request.PlanId);
            var subarea = await _subareaRepository.GetSubareaAsync(request.SubareaId);

            if(plan == null)
                throw new BadRequestException("Nie znaleziono planu o podanym Id");

            if (subarea == null)
                throw new BadRequestException("Nie znaleziono wydzielenia o podanym Id");

            if (subarea.Division.Id == plan.ForestUnitId)
                throw new BadRequestException("Podany plan i wydzielenie należa do różnych leśnictw");

            if (!await CheckAssignedForestUnit(plan.ForestUnit))
                throw new ForbiddenException("Nie masz uprawnień do tego leśnictwa");

            if (plan.IsCompleted)
                throw new BadRequestException("Plan został już ukończony - dodawanie nowych pozycji planu zablokowane");

            var checkDuplicate = _planItemRepository.GetPlanItems().FirstOrDefault(p =>
                p.PlanId == request.PlanId &&
                p.SubareaId == request.SubareaId &&
                p.ActionGroup == request.ActionGroup
            );

            if(checkDuplicate != null)
                throw new BadRequestException("Taka pozycja planu już istnieje");

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

            await _planItemRepository.AddPlanItemAsync(planItem);

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