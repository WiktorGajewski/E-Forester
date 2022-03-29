using E_Forester.Application.DataTransferObjects.PlanItems;
using E_Forester.Application.Pagination.Wrappers;
using MediatR;

namespace E_Forester.Application.Content.PlanItems.Queries.GetPlanItemsQuery
{
    public class GetPlanItemsQuery : IRequest<Page<PlanItemDto>>
    {
        public int? PageIndex { get; set; } = null;
        public int? PageSize { get; set; } = null;
        public int? ForestUnitId { get; set; } = null;
        public int? DivisionId { get; set; } = null;
        public int? SubareaId { get; set; } = null;
        public int? PlanId { get; set; } = null;
        public bool FilterByNotCompleted { get; set; } = false;
    }
}
