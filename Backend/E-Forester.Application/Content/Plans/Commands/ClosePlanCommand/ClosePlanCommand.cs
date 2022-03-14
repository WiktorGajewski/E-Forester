using MediatR;

namespace E_Forester.Application.Content.Plans.Commands.ClosePlanCommand
{
    public class ClosePlanCommand : IRequest
    {
        public int Id { get; set; }
    }
}
