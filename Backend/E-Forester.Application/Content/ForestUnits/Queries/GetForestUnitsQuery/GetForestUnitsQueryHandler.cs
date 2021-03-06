using AutoMapper;
using E_Forester.Application.DataTransferObjects.ForestUnits;
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

namespace E_Forester.Application.Content.ForestUnits.Queries.GetForestUnitsQuery
{
    public class GetForestUnitsQueryHandler : IRequestHandler<GetForestUnitsQuery, Page<ForestUnitDto>>
    {
        private readonly IForestUnitRepository _forestUnitRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public GetForestUnitsQueryHandler(IForestUnitRepository forestUnitRepository, IAuthService authService, IUserRepository userRepository, IMapper mapper)
        {
            _forestUnitRepository = forestUnitRepository;
            _mapper = mapper;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<Page<ForestUnitDto>> Handle(GetForestUnitsQuery request, CancellationToken cancellationToken)
        {
            var forestUnitsQuery = _forestUnitRepository.GetForestUnits();

            var forestUnits = new List<ForestUnit>();

            forestUnitsQuery = await FilterAssignedForestUnits(forestUnitsQuery);

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                forestUnits = await SelectPage(forestUnitsQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else
            {
                forestUnits = await forestUnitsQuery
                    .OrderBy(f => f.Address)
                    .ToListAsync();
            }

            var forestUnitDtos = _mapper.Map<ICollection<ForestUnit>, ICollection<ForestUnitDto>>(forestUnits);

            int total = forestUnitsQuery.Count();

            return new Page<ForestUnitDto>(forestUnitDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<ForestUnit>> SelectPage(IQueryable<ForestUnit> forestUnitsQuery, int pageIndex, int pageSize)
        {
            return await forestUnitsQuery
                    .OrderBy(f => f.Address)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        private async Task<IQueryable<ForestUnit>> FilterAssignedForestUnits(IQueryable<ForestUnit> forestUnitsQuery)
        {
            if (_authService.GetCurrentUserRole() != UserRole.Admin)
            {
                var id = _authService.GetCurrentUserId();
                var user = await _userRepository.GetUserAsync(id);

                forestUnitsQuery = forestUnitsQuery.Where(x => user.AssignedForestUnits.Contains(x));
            }

            return forestUnitsQuery;
        }
    }
}
