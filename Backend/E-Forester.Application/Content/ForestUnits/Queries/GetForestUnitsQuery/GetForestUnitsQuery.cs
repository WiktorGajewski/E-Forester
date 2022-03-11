using E_Forester.Application.DataTransferObjects.ForestUnits;
using E_Forester.Application.Pagination.Wrappers;
using MediatR;

namespace E_Forester.Application.Content.ForestUnits.Queries.GetForestUnitsQuery
{
    public class GetForestUnitsQuery : IRequest<Page<ForestUnitDto>>
    {
        public int? PageIndex { get; set; } = null;
        public int? PageSize { get; set; } = null;
    }
}
