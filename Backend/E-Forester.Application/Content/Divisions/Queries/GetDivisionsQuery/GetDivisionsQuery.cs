using E_Forester.Application.DataTransferObjects.Divisions;
using E_Forester.Application.Pagination.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.Divisions.Queries.GetDivisionsQuery
{
    public class GetDivisionsQuery : IRequest<Page<ICollection<DivisionDto>>>
    {
        public int? PageIndex { get; set; } = null;
        public int? PageSize { get; set; } = null;
    }
}
