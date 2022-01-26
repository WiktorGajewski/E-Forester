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
        private readonly IMapper _mapper;

        public GetPlanItemsQueryHandler(IPlanItemRepository planItemRepository, IMapper mapper)
        {
            _planItemRepository = planItemRepository;
            _mapper = mapper;
        }

        public async Task<Page<PlanItemDto>> Handle(GetPlanItemsQuery request, CancellationToken cancellationToken)
        {
            var planItemsQuery = _planItemRepository.GetPlanItems();

            var planItems = new List<PlanItem>();

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                planItems = await SelectPage(planItemsQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else
            {
                planItems = await planItemsQuery
                    .OrderBy(d => d.Id)
                    .ToListAsync();
            }

            var planItemsDtos = _mapper.Map<ICollection<PlanItem>, ICollection<PlanItemDto>>(planItems);

            int total = planItemsQuery.Count();

            return new Page<PlanItemDto>(planItemsDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<PlanItem>> SelectPage(IQueryable<PlanItem> planItemsQuery, int pageIndex, int pageSize)
        {
            return await planItemsQuery
                    .OrderBy(d => d.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }
    }
}
