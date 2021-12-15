using MediatR;

namespace E_Forester.Application.Content.PlanExecutions.Commands.CreatePlanExecution
{
    public class CreatePlanExecutionCommand : IRequest
    {
        public double Quantity { get; set; }
        public int PlanItemId { get; set; }
        public int PlanId { get; set; }
    }
}
