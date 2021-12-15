using E_Forester.Application.DataTransferObjects.Subareas;
using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.Subareas.Queries.GetSubareasQuery
{
    public class GetSubareasQuery : IRequest<ICollection<SubareaDto>>
    {

    }
}
