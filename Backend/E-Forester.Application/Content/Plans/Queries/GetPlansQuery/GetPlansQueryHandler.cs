using AutoMapper;
using E_Forester.Application.DataTransferObjects.Plans;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
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
        private readonly IMapper _mapper;

        public GetPlansQueryHandler(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<Page<PlanDto>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
            var plansQuery = _planRepository.GetPlans();

            var plans = new List<Plan>();

            if (request.ForestUnitId != null)
            {
                plansQuery = plansQuery.Where(d => d.ForestUnitId == request.ForestUnitId);
            }

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                plans = await SelectPage(plansQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else
            {
                plans = await plansQuery
                    .OrderByDescending(p => p.Year)
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
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }
    }
}