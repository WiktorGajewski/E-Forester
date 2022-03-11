using MediatR;

namespace E_Forester.Application.Content.Plans.Commands.OpenPlanCommand
{
    public class OpenPlanCommand : IRequest
    {
        public int Id { get; set; }
    }
}
