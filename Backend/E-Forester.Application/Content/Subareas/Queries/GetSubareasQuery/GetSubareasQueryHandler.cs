using AutoMapper;
using E_Forester.Application.DataTransferObjects.Subareas;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Subareas.Queries.GetSubareasQuery
{
    public class GetSubareasQueryHandler : IRequestHandler<GetSubareasQuery, ICollection<SubareaDto>>
    {
        private readonly ISubareaRepository _subareaRepository;
        private readonly IMapper _mapper;

        public GetSubareasQueryHandler(ISubareaRepository subareaRepository, IMapper mapper)
        {
            _subareaRepository = subareaRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<SubareaDto>> Handle(GetSubareasQuery request, CancellationToken cancellationToken)
        {
            var subareas = await _subareaRepository.GetSubareasAsync();
            return _mapper.Map<ICollection<Subarea>, ICollection<SubareaDto>>(subareas);
        }
    }
}
