using E_Forester.Application.DataTransferObjects.Plans;
using E_Forester.Application.Pagination.Wrappers;
using MediatR;

namespace E_Forester.Application.Content.Plans.Queries.GetPlansQuery
{
    public class GetPlansQuery : IRequest<Page<PlanDto>>
    {
        public int? PageIndex { get; set; } = null;
        public int? PageSize { get; set; } = null;
        public int? ForestUnitId { get; set; } = null;
    }
}
