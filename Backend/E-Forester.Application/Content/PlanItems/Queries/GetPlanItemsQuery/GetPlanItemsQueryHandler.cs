using AutoMapper;
using E_Forester.Application.DataTransferObjects.PlanItems;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanItems.Queries.GetPlanItemsQuery
{
    public class GetPlanItemsQueryHandler : IRequestHandler<GetPlanItemsQuery, ICollection<PlanItemDto>>
    {
        private readonly IPlanItemRepository _planItemRepository;
        private readonly IMapper _mapper;

        public GetPlanItemsQueryHandler(IPlanItemRepository planItemRepository, IMapper mapper)
        {
            _planItemRepository = planItemRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<PlanItemDto>> Handle(GetPlanItemsQuery request, CancellationToken cancellationToken)
        {
            var planItems = await _planItemRepository.GetPlanItemsAsync();
            return _mapper.Map<ICollection<PlanItem>, ICollection<PlanItemDto>>(planItems);
        }
    }
}
