using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.PlanExecutions.Commands.CreatePlanExecution
{
    public class CreatePlanExecutionCommand : IRequest
    {
        [Required]
        public double ExecutedHectares { get; set; }

        [Required]
        public double HarvestedCubicMeters { get; set; }

        [Required]
        public int PlanItemId { get; set; }

        [Required]
        public int PlanId { get; set; }
    }
}
