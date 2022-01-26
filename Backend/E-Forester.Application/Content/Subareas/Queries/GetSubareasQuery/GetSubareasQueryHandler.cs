using AutoMapper;
using E_Forester.Application.DataTransferObjects.Subareas;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
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

        public GetSubareasQueryHandler(ISubareaRepository subareaRepository, IMapper mapper)
        {
            _subareaRepository = subareaRepository;
            _mapper = mapper;
        }

        public async Task<Page<SubareaDto>> Handle(GetSubareasQuery request, CancellationToken cancellationToken)
        {
            var subareasQuery = _subareaRepository.GetSubareas();

            var subareas = new List<Subarea>();

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

            var subareasDtos = _mapper.Map<ICollection<Subarea>, ICollection<SubareaDto>>(subareas);

            int total = subareasQuery.Count();

            return new Page<SubareaDto>(subareasDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<Subarea>> SelectPage(IQueryable<Subarea> subareasQuery, int pageIndex, int pageSize)
        {
            return await subareasQuery
                    .OrderBy(s => s.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }
    }
}
