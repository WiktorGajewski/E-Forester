using E_Forester.Application.DataTransferObjects.Plans;
using MediatR;

namespace E_Forester.Application.Content.Plans.Queries.GetPlanQuery
{
    public class GetPlanQuery : IRequest<PlanDto>
    {
        public int Id { get; set; }
    }
}
