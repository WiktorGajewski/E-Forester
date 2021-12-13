using AutoMapper;
using E_Forester.Application.DataTransferObjects.Plans;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Plans.Queries.GetPlansQuery
{
    public class GetPlansQueryHandler : IRequestHandler<GetPlansQuery, ICollection<PlanDto>>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public GetPlansQueryHandler(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<PlanDto>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
            var plans = await _planRepository.GetPlansAsync();
            return _mapper.Map<ICollection<Plan>, ICollection<PlanDto>>(plans);
        }
    }
}