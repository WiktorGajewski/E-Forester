using E_Forester.Application.DataTransferObjects.Divisions;
using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.Divisions.Queries.GetDivisionsQuery
{
    public class GetDivisionsQuery : IRequest<ICollection<DivisionDto>>
    {

    }
}
