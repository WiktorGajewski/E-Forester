using AutoMapper;
using E_Forester.Application.CustomExceptions;
using E_Forester.Application.DataTransferObjects.PlanItems;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanItems.Queries.GetPlanItem
{
    public class GetPlanItemQueryHandler : IRequestHandler<GetPlanItemQuery, PlanItemDto>
    {
        private readonly IPlanItemRepository _planItemRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public GetPlanItemQueryHandler(IPlanItemRepository planItemRepository, IAuthService authService, IUserRepository userRepository, IMapper mapper)
        {
            _planItemRepository = planItemRepository;
            _mapper = mapper;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<PlanItemDto> Handle(GetPlanItemQuery request, CancellationToken cancellationToken)
        {
            var planItem = await _planItemRepository.GetPlanItems()
                .Include(p => p.PlanExecutions)
                .Include(p => p.Subarea)
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (planItem == null)
                throw new NotFoundException("Nie znaleziono pozycji planu o podanym Id");

            if (!await CheckAssignedForestUnit(planItem.Plan.ForestUnitId))
                throw new ForbiddenException("Nie masz uprawnień do tego leśnictwa");

            var planItemDto = _mapper.Map<PlanItem, PlanItemDto>(planItem);

            return planItemDto;
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
