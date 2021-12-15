using AutoMapper;
using E_Forester.Application.DataTransferObjects.PlanExecutions;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanExecutions.Queries.GetPlanExecutions
{
    public class GetPlanExecutionsQueryHandler : IRequestHandler<GetPlanExecutionsQuery, ICollection<PlanExecutionDto>>
    {
        private readonly IPlanExecutionRepository _planExecutionRepository;
        private readonly IMapper _mapper;

        public GetPlanExecutionsQueryHandler(IPlanExecutionRepository planExecutionRepository, IMapper mapper)
        {
            _planExecutionRepository = planExecutionRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<PlanExecutionDto>> Handle(GetPlanExecutionsQuery request, CancellationToken cancellationToken)
        {
            var planExecutions = await _planExecutionRepository.GetPlanExecutionsAsync();
            return _mapper.Map<ICollection<PlanExecution>, ICollection<PlanExecutionDto>>(planExecutions);
        }
    }
}
