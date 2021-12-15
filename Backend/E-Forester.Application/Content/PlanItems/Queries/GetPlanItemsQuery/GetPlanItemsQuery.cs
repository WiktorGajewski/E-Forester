using E_Forester.Application.DataTransferObjects.PlanItems;
using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.PlanItems.Queries.GetPlanItemsQuery
{
    public class GetPlanItemsQuery : IRequest<ICollection<PlanItemDto>>
    {

    }
}
