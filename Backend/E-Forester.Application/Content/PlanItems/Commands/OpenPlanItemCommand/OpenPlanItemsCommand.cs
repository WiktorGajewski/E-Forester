using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.PlanItems.Commands.OpenPlanItemCommand
{
    public class OpenPlanItemsCommand : IRequest
    {
        [Required]
        public List<int> planItemIds { get; set; }
    }
}
