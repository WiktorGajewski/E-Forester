using AutoMapper;
using E_Forester.Application.DataTransferObjects.ForestUnits;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.ForestUnits.Queries.GetForestUnitsQuery
{
    public class GetForestUnitsQueryHandler : IRequestHandler<GetForestUnitsQuery, ICollection<ForestUnitDto>>
    {
        private readonly IForestUnitRepository _forestUnitRepository;
        private readonly IMapper _mapper;

        public GetForestUnitsQueryHandler(IForestUnitRepository forestUnitRepository, IMapper mapper)
        {
            _forestUnitRepository = forestUnitRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ForestUnitDto>> Handle(GetForestUnitsQuery request, CancellationToken cancellationToken)
        {
            var forestUnits = await _forestUnitRepository.GetForestUnitsAsync();
            return _mapper.Map<ICollection<ForestUnit>, ICollection<ForestUnitDto>>(forestUnits);
        }
    }
}
