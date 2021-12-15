using E_Forester.Application.DataTransferObjects.PlanExecutions;
using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.PlanExecutions.Queries.GetPlanExecutions
{
    public class GetPlanExecutionsQuery : IRequest<ICollection<PlanExecutionDto>>
    {

    }
}
