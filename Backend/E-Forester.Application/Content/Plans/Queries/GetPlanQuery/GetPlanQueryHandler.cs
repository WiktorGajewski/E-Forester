using AutoMapper;
using E_Forester.Application.DataTransferObjects.Plans;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Plans.Queries.GetPlanQuery
{
    public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, PlanDto>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public GetPlanQueryHandler(IPlanRepository planRepository, IMapper mapper)
        {
            this._planRepository = planRepository;
            this._mapper = mapper;
        }

        public async Task<PlanDto> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanAsync(request.Id);

            var planDto = _mapper.Map<Plan, PlanDto>(plan);

            return planDto;
        }
    }
}
