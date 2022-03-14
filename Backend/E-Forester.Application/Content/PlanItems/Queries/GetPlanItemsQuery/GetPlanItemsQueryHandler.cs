using AutoMapper;
using E_Forester.Application.DataTransferObjects.PlanItems;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
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
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public GetPlanItemsQueryHandler(IPlanItemRepository planItemRepository, IAuthService authService, IUserRepository userRepository, IMapper mapper)
        {
            _planItemRepository = planItemRepository;
            _mapper = mapper;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<Page<PlanItemDto>> Handle(GetPlanItemsQuery request, CancellationToken cancellationToken)
        {
            var planItemsQuery = _planItemRepository.GetPlanItems();

            var planItems = new List<PlanItem>();

            planItemsQuery = await FilterAssignedForestUnits(planItemsQuery);

            planItemsQuery = Filter(planItemsQuery, request.ForestUnitId, request.DivisionId, request.SubareaId, request.PlanId);

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                planItems = await SelectPage(planItemsQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else
            {
                planItems = await planItemsQuery
                    .OrderBy(p => p.Subarea.Address)
                    .ThenBy(p => p.Id)
                    .ToListAsync();
            }

            var planItemDtos = _mapper.Map<ICollection<PlanItem>, ICollection<PlanItemDto>>(planItems);

            int total = planItemsQuery.Count();

            return new Page<PlanItemDto>(planItemDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<PlanItem>> SelectPage(IQueryable<PlanItem> planItemsQuery, int pageIndex, int pageSize)
        {
            return await planItemsQuery
                    .OrderBy(p => p.Subarea.Address)
                    .ThenBy(p => p.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Include(p => p.Subarea)
                    .Include(p => p.PlanExecutions)
                    .ToListAsync();
        }

        private IQueryable<PlanItem> Filter(IQueryable<PlanItem> planItemsQuery, int? forestUnitId, int? divisionId, int? subareaId, int? planId)
        {
            if (forestUnitId != null)
            {
                planItemsQuery = planItemsQuery.Where(p => p.Plan.ForestUnitId == (int)forestUnitId);
            }

            if (divisionId != null)
            {
                planItemsQuery = planItemsQuery.Where(p => p.Subarea.DivisionId == (int)divisionId);
            }

            if (subareaId != null)
            {
                planItemsQuery = planItemsQuery.Where(d => d.SubareaId == subareaId);
            }

            if (planId != null)
            {
                planItemsQuery = planItemsQuery.Where(d => d.PlanId == planId);
            }

            return planItemsQuery;
        }

        private async Task<IQueryable<PlanItem>> FilterAssignedForestUnits(IQueryable<PlanItem> planItemsQuery)
        {
            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var id = _authService.GetCurrentUserId();
                var user = await _userRepository.GetUserAsync(id);

                planItemsQuery = planItemsQuery.Where(x => user.AssignedForestUnits.Contains(x.Plan.ForestUnit));
            }

            return planItemsQuery;
        }
    }
}
