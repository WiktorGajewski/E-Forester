using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.PlanItems.Commands.ClosePlanItemCommand
{
    public class ClosePlanItemsCommand : IRequest
    {
        public List<int> planItemIds { get; set; }
    }
}
