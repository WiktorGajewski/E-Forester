using AutoMapper;
using E_Forester.Application.DataTransferObjects.PlanItems;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanItems.Queries.GetPlanItemsQuery
{
    public class GetPlanItemsQueryHandler : IRequestHandler<GetPlanItemsQuery, Page<PlanItemDto>>
    {
        private readonly IPlanItemRepository _planItemRepository;
        private readonly IDivisionRepository _divisionRepository;
        private readonly IForestUnitRepository _forestUnitRepository;

        private readonly IMapper _mapper;

        public GetPlanItemsQueryHandler(IPlanItemRepository planItemRepository, 
            IDivisionRepository divisionRepository,
            IForestUnitRepository forestUnitRepository,
            IMapper mapper)
        {
            _planItemRepository = planItemRepository;
            _divisionRepository = divisionRepository;
            _forestUnitRepository = forestUnitRepository;
            _mapper = mapper;
        }

        public async Task<Page<PlanItemDto>> Handle(GetPlanItemsQuery request, CancellationToken cancellationToken)
        {
            var planItemsQuery = _planItemRepository.GetPlanItems();

            var planItems = new List<PlanItem>();

            planItemsQuery = Filter(planItemsQuery, request.ForestUnitId, request.DivisionId, request.SubareaId, request.PlanId);

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                planItems = await SelectPage(planItemsQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else
            {
                planItems = await planItemsQuery
                    .OrderBy(p => p.Id)
                    .ToListAsync();
            }

            var planItemDtos = _mapper.Map<ICollection<PlanItem>, ICollection<PlanItemDto>>(planItems);

            int total = planItemsQuery.Count();

            return new Page<PlanItemDto>(planItemDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<PlanItem>> SelectPage(IQueryable<PlanItem> planItemsQuery, int pageIndex, int pageSize)
        {
            return await planItemsQuery
                    .OrderBy(p => p.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        private IQueryable<PlanItem> Filter(IQueryable<PlanItem> planItemsQuery, int? forestUnitId, int? divisionId, int? subareaId, int? planId)
        {
            if (subareaId != null)
            {
                planItemsQuery = planItemsQuery.Where(d => d.SubareaId == subareaId);
            }
            else if (divisionId != null)
            {
                planItemsQuery = planItemsQuery.Where(p => p.Subarea.DivisionId == (int)divisionId);
            }
            else if (forestUnitId != null)
            {
                planItemsQuery = planItemsQuery.Where(p => p.Plan.ForestUnitId == (int)forestUnitId);
            }

            if (planId != null)
            {
                planItemsQuery = planItemsQuery.Where(d => d.PlanId == planId);
            }

            return planItemsQuery;
        }
    }
}
