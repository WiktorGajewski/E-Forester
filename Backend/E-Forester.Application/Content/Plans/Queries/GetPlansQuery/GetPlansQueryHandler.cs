using AutoMapper;
using E_Forester.Application.DataTransferObjects.Plans;
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

namespace E_Forester.Application.Content.Plans.Queries.GetPlansQuery
{
    public class GetPlansQueryHandler : IRequestHandler<GetPlansQuery, Page<PlanDto>>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetPlansQueryHandler(IPlanRepository planRepository, IAuthService authService, IUserRepository userRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<Page<PlanDto>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
            var plansQuery = _planRepository.GetPlans();

            var plans = new List<Plan>();

            plansQuery = await FilterAssignedForestUnits(plansQuery);

            plansQuery = Filter(plansQuery, request.ForestUnitId, request.YearFrom, request.YearTo);

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                plans = await SelectPage(plansQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else if(request.YearFrom != null && request.YearTo != null)
            {
                plans = await plansQuery
                    .OrderByDescending(p => p.Year)
                    .ThenBy(p => p.ForestUnitId)
                    .Include(p => p.ForestUnit)
                    .Include(p => p.PlanExecutions)
                    .Include(p => p.PlanItems)
                    .ToListAsync();
            }
            else
            {
                plans = await plansQuery
                    .OrderByDescending(p => p.Year)
                    .ThenBy(p => p.ForestUnitId)
                    .ToListAsync();
            }

            var planDtos = _mapper.Map<ICollection<Plan>, ICollection<PlanDto>>(plans);

            int total = plansQuery.Count();

            return new Page<PlanDto>(planDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<Plan>> SelectPage(IQueryable<Plan> plansQuery, int pageIndex, int pageSize)
        {
            return await plansQuery
                    .OrderByDescending(p => p.Year)
                    .ThenBy(p => p.ForestUnitId)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Include(p => p.ForestUnit)
                    .Include(p => p.PlanExecutions)
                    .Include(p => p.PlanItems)
                    .ToListAsync();
        }

        private IQueryable<Plan> Filter(IQueryable<Plan> plansQuery, int? forestUnitId, int? yearFrom, int? yearTo)
        {
            if (forestUnitId != null)
            {
                plansQuery = plansQuery.Where(d => d.ForestUnitId == forestUnitId);
            }

            if (yearFrom != null)
            {
                plansQuery = plansQuery.Where(d => d.Year >= yearFrom);
            }

            if (yearTo != null)
            {
                plansQuery = plansQuery.Where(d => d.Year <= yearTo);
            }

            return plansQuery;
        }

        private async Task<IQueryable<Plan>> FilterAssignedForestUnits(IQueryable<Plan> plansQuery)
        {
            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var id = _authService.GetCurrentUserId();
                var user = await _userRepository.GetUserAsync(id);

                plansQuery = plansQuery.Where(x => user.AssignedForestUnits.Contains(x.ForestUnit));
            }

            return plansQuery;
        }
    }
}