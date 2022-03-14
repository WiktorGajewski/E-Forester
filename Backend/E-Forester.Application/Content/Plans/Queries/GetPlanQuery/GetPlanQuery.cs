using E_Forester.Application.DataTransferObjects.Plans;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Application.Content.Plans.Queries.GetPlanQuery
{
    public class GetPlanQuery : IRequest<PlanDto>
    {
        [Required]
        public int Id { get; set; }
    }
}
