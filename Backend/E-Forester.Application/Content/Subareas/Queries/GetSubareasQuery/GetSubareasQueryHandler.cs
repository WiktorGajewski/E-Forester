using AutoMapper;
using E_Forester.Application.DataTransferObjects.Subareas;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Infrastructure.Interfaces;
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
        private readonly IUserRepository _userRepository;

        public GetSubareasQueryHandler(ISubareaRepository subareaRepository, IAuthService authService, IUserRepository userRepository, IMapper mapper)
        {
            _subareaRepository = subareaRepository;
            _mapper = mapper;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<Page<SubareaDto>> Handle(GetSubareasQuery request, CancellationToken cancellationToken)
        {
            var subareasQuery = _subareaRepository.GetSubareas();

            var subareas = new List<Subarea>();

            subareasQuery = await FilterAssignedForestUnits(subareasQuery);

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

        private async Task<IQueryable<Subarea>> FilterAssignedForestUnits(IQueryable<Subarea> subareasQuery)
        {
            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var id = _authService.GetCurrentUserId();
                var user = await _userRepository.GetUserAsync(id);

                subareasQuery = subareasQuery.Where(x => user.AssignedForestUnits.Contains(x.Division.ForestUnit));
            }

            return subareasQuery;
        }
    }
}
