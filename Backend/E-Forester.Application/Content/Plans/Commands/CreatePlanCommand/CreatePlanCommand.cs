using MediatR;

namespace E_Forester.Application.Content.Plans.Commands.CreatePlanCommand
{
    public class CreatePlanCommand : IRequest
    {
        public int Year { get; set; }
        public int ForestUnitId { get; set; }

    }
}