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

namespace E_Forester.Application.Content.Plans.Commands.CreatePlanCommand
{
    public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public CreatePlanCommandHandler(IPlanRepository planRepository, IAuthService authService, IUserRepository userRepository)
        {
            _planRepository = planRepository;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            if (!await CheckAssignedForestUnit(request.ForestUnitId))
                throw new ForbiddenException();

            var plansQuery = _planRepository.GetPlans();

            var checkDuplicate = plansQuery.FirstOrDefault(p => p.ForestUnitId == request.ForestUnitId && p.Year == p.Year);
            
            if (checkDuplicate != null)
                throw new BadRequestException("Plan for this year and this forest unit already exists");

            var plan = new Plan()
            {
                Year = request.Year,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                ForestUnitId = request.ForestUnitId,
                CreatorId = _authService.GetCurrentUserId()
            };

            await _planRepository.AddPlanAsync(plan);

            return await Task.FromResult(Unit.Value);
        }

        private async Task<bool> CheckAssignedForestUnit(int checkForestUnitId)
        {
            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var id = _authService.GetCurrentUserId();
                var user = await _userRepository.GetUserAsync(id);

                if (!user.AssignedForestUnits.Any(x => x.Id == checkForestUnitId))
                    return false;
            }

            return true;
        }
    }
}