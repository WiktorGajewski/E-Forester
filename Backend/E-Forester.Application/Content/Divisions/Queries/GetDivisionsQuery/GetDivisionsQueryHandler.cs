using AutoMapper;
using E_Forester.Application.DataTransferObjects.Divisions;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Divisions.Queries.GetDivisionsQuery
{
    public class GetDivisionsQueryHandler : IRequestHandler<GetDivisionsQuery, ICollection<DivisionDto>>
    {
        private readonly IDivisionRepository _divisionRepository;
        private readonly IMapper _mapper;

        public GetDivisionsQueryHandler(IDivisionRepository divisionRepository, IMapper mapper)
        {
            _divisionRepository = divisionRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<DivisionDto>> Handle(GetDivisionsQuery request, CancellationToken cancellationToken)
        {
            var divisions = await _divisionRepository.GetDivisionsAsync();
            return _mapper.Map<ICollection<Division>, ICollection<DivisionDto>>(divisions);
        }
    }
}
