using AutoMapper;
using E_Forester.Application.DataTransferObjects.Subareas;
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

namespace E_Forester.Application.Content.Subareas.Queries.GetSubareasQuery
{
    public class GetSubareasQueryHandler : IRequestHandler<GetSubareasQuery, Page<SubareaDto>>
    {
        private readonly ISubareaRepository _subareaRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public GetSubareasQueryHandler(ISubareaRepository subareaRepository, IMapper mapper, IAuthService authService)
        {
            _subareaRepository = subareaRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<Page<SubareaDto>> Handle(GetSubareasQuery request, CancellationToken cancellationToken)
        {
            var subareasQuery = _subareaRepository.GetSubareas();

            var subareas = new List<Subarea>();

            subareasQuery = await FilterAuth(subareasQuery);

            if (request.ForestUnitId != null)
            {
                subareasQuery = subareasQuery.Where(p => p.Division.ForestUnitId == (int)request.ForestUnitId);
            }

            if (request.DivisionId != null)
            {
                subareasQuery = subareasQuery.Where(s => s.DivisionId == request.DivisionId);
            }

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                subareas = await SelectPage(subareasQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else
            {
                subareas = await subareasQuery
                    .OrderBy(s => s.Id)
                    .ToListAsync();
            }

            var subareaDtos = _mapper.Map<ICollection<Subarea>, ICollection<SubareaDto>>(subareas);

            int total = subareasQuery.Count();

            return new Page<SubareaDto>(subareaDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<Subarea>> SelectPage(IQueryable<Subarea> subareasQuery, int pageIndex, int pageSize)
        {
            return await subareasQuery
                    .OrderBy(s => s.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        private async Task<IQueryable<Subarea>> FilterAuth(IQueryable<Subarea> subareasQuery)
        {
            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var assignedForestUnits = await _authService.GetAssignedForestUnits();

                subareasQuery = subareasQuery.Where(x => assignedForestUnits.Contains(x.Division.ForestUnit));
            }

            return subareasQuery;
        }
    }
}
