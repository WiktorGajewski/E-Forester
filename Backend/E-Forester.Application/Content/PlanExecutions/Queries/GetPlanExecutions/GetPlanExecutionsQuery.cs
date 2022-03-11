using E_Forester.Application.DataTransferObjects.PlanExecutions;
using E_Forester.Application.Pagination.Wrappers;
using MediatR;

namespace E_Forester.Application.Content.PlanExecutions.Queries.GetPlanExecutions
{
    public class GetPlanExecutionsQuery : IRequest<Page<PlanExecutionDto>>
    {
        public int? PageIndex { get; set; } = null;
        public int? PageSize { get; set; } = null;
        public int? PlanItemId { get; set; } = null;
        public int? PlanId { get; set; } = null;
    }
}
