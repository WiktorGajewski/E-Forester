using AutoMapper;
using E_Forester.Application.CustomExceptions;
using E_Forester.Application.DataTransferObjects.Plans;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
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

        public GetPlanQueryHandler(IPlanRepository planRepository, IMapper mapper, IAuthService authService)
        {
            _planRepository = planRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<PlanDto> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanAsync(request.Id);

            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var assignedForestUnits = await _authService.GetAssignedForestUnits();

                if(!assignedForestUnits.Contains(plan.ForestUnit))
                    throw new ForbiddenException();
            }

            var planDto = _mapper.Map<Plan, PlanDto>(plan);

            return planDto;
        }
    }
}
