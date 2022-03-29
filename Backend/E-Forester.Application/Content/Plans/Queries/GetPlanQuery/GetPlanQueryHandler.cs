using AutoMapper;
using E_Forester.Application.CustomExceptions;
using E_Forester.Application.DataTransferObjects.Plans;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Plans.Queries.GetPlanQuery
{
    public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, PlanDto>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public GetPlanQueryHandler(IPlanRepository planRepository, IAuthService authService, IUserRepository userRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<PlanDto> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanAsync(request.Id);

            if(plan == null)
                throw new NotFoundException("Nie znaleziono planu o podanym Id");

            if(!await CheckAssignedForestUnit(plan.ForestUnit))
                throw new ForbiddenException("Nie masz uprawnień do tego leśnictwa");

            var planDto = _mapper.Map<Plan, PlanDto>(plan);

            return planDto;
        }

        private async Task<bool> CheckAssignedForestUnit(ForestUnit checkForestUnit)
        {
            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var id = _authService.GetCurrentUserId();
                var user = await _userRepository.GetUserAsync(id);

                if(!user.AssignedForestUnits.Contains(checkForestUnit))
                    return false;
            }

            return true;
        }
    }
}
