using AutoMapper;
using E_Forester.Application.DataTransferObjects.Divisions;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Divisions.Queries.GetDivisionsQuery
{
    public class GetDivisionsQueryHandler : IRequestHandler<GetDivisionsQuery, Page<DivisionDto>>
    {
        private readonly IDivisionRepository _divisionRepository;
        private readonly IMapper _mapper;

        public GetDivisionsQueryHandler(IDivisionRepository divisionRepository, IMapper mapper)
        {
            _divisionRepository = divisionRepository;
            _mapper = mapper;
        }

        public async Task<Page<DivisionDto>> Handle(GetDivisionsQuery request, CancellationToken cancellationToken)
        {
            var divisionsQuery = _divisionRepository.GetDivisions();

            var divisions = new List<Division>();

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                divisions = await SelectPage(divisionsQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else
            {
                divisions = await divisionsQuery
                    .OrderBy(d => d.Address)
                    .ToListAsync();
            }

            var divisionDtos = _mapper.Map<ICollection<Division>, ICollection<DivisionDto>>(divisions);

            int total = divisionsQuery.Count();

            return new Page<DivisionDto>(divisionDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<Division>> SelectPage(IQueryable<Division> divisionsQuery, int pageIndex, int pageSize)
        {
            return await divisionsQuery
                    .OrderBy(d => d.Address)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }
    }
}
