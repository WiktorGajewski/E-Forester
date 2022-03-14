using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.PlanItems.Commands.ClosePlanItemCommand
{
    public class ClosePlanItemsCommand : IRequest
    {
        [Required]
        public List<int> planItemIds { get; set; }
    }
}
