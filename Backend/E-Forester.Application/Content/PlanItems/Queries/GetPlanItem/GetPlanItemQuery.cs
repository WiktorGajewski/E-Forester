using E_Forester.Application.DataTransferObjects.PlanItems;
using MediatR;

namespace E_Forester.Application.Content.PlanItems.Queries.GetPlanItem
{
    public class GetPlanItemQuery : IRequest<PlanItemDto>
    {
        public int Id { get; set; }
    }
}
