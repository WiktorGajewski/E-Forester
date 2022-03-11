using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.PlanItems.Commands.OpenPlanItemCommand
{
    public class OpenPlanItemsCommand : IRequest
    {
        public List<int> planItemIds { get; set; }
    }
}
