using E_Forester.Application.DataTransferObjects.ForestUnits;
using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.ForestUnits.Queries.GetForestUnitsQuery
{
    public class GetForestUnitsQuery : IRequest<ICollection<ForestUnitDto>>
    {

    }
}
