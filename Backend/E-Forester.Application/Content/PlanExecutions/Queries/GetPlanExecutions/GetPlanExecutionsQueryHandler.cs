using AutoMapper;
using E_Forester.Application.DataTransferObjects.PlanExecutions;
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

namespace E_Forester.Application.Content.PlanExecutions.Queries.GetPlanExecutions
{
    public class GetPlanExecutionsQueryHandler : IRequestHandler<GetPlanExecutionsQuery, Page<PlanExecutionDto>>
    {
        private readonly IPlanExecutionRepository _planExecutionRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public GetPlanExecutionsQueryHandler(IPlanExecutionRepository planExecutionRepository, IMapper mapper, IAuthService authService)
        {
            _planExecutionRepository = planExecutionRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<Page<PlanExecutionDto>> Handle(GetPlanExecutionsQuery request, CancellationToken cancellationToken)
        {
            var planExecutionsQuery = _planExecutionRepository.GetPlanExecutions();

            var planExecutions = new List<PlanExecution>();

            planExecutionsQuery = await FilterAuth(planExecutionsQuery);

            if (request.PlanItemId != null)
            {
                planExecutionsQuery = planExecutionsQuery.Where(d => d.PlanItemId == request.PlanItemId);
            }

            if (request.PlanId != null)
            {
                planExecutionsQuery = planExecutionsQuery.Where(d => d.PlanId == request.PlanId);
            }

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                planExecutions = await SelectPage(planExecutionsQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else
            {
                planExecutions = await planExecutionsQuery
                    .OrderBy(p => p.Id)
                    .ToListAsync();
            }

            var planExecutionDtos = _mapper.Map<ICollection<PlanExecution>, ICollection<PlanExecutionDto>>(planExecutions);

            int total = planExecutionsQuery.Count();

            return new Page<PlanExecutionDto>(planExecutionDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<PlanExecution>> SelectPage(IQueryable<PlanExecution> planExecutionsQuery, int pageIndex, int pageSize)
        {
            return await planExecutionsQuery
                    .OrderBy(p => p.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        private async Task<IQueryable<PlanExecution>> FilterAuth(IQueryable<PlanExecution> planExecutionsQuery)
        {
            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var assignedForestUnits = await _authService.GetAssignedForestUnits();

                planExecutionsQuery = planExecutionsQuery.Where(x => assignedForestUnits.Contains(x.Plan.ForestUnit));
            }

            return planExecutionsQuery;
        }
    }
}
