using E_Forester.Application.DataTransferObjects.ForestUnits;
using E_Forester.Application.Pagination.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.ForestUnits.Queries.GetForestUnitsQuery
{
    public class GetForestUnitsQuery : IRequest<Page<ICollection<ForestUnitDto>>>
    {
        public int? PageIndex { get; set; } = null;
        public int? PageSize { get; set; } = null;
    }
}
