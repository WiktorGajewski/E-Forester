using E_Forester.Application.DataTransferObjects.Plans;
using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.Plans.Queries.GetPlansQuery
{
    public class GetPlansQuery : IRequest<ICollection<PlanDto>>
    {

    }
}
