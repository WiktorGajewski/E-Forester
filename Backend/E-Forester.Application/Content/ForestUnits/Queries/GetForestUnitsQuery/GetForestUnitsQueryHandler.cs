using AutoMapper;
using E_Forester.Application.DataTransferObjects.ForestUnits;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
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

        public GetForestUnitsQueryHandler(IForestUnitRepository forestUnitRepository, IMapper mapper)
        {
            _forestUnitRepository = forestUnitRepository;
            _mapper = mapper;
        }

        public async Task<Page<ForestUnitDto>> Handle(GetForestUnitsQuery request, CancellationToken cancellationToken)
        {
            var forestUnitsQuery = _forestUnitRepository.GetForestUnits();

            var forestUnits = new List<ForestUnit>();

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                forestUnits = await SelectPage(forestUnitsQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else
            {
                forestUnits = await forestUnitsQuery
                    .OrderBy(d => d.Address)
                    .ToListAsync();
            }

            var forestUnitsDtos = _mapper.Map<ICollection<ForestUnit>, ICollection<ForestUnitDto>>(forestUnits);

            int total = forestUnitsQuery.Count();

            return new Page<ForestUnitDto>(forestUnitsDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<ForestUnit>> SelectPage(IQueryable<ForestUnit> forestUnitsQuery, int pageIndex, int pageSize)
        {
            return await forestUnitsQuery
                    .OrderBy(d => d.Address)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }
    }
}
