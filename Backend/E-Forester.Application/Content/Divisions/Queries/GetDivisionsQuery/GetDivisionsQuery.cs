using E_Forester.Application.DataTransferObjects.Divisions;
using E_Forester.Application.Pagination.Wrappers;
using MediatR;

namespace E_Forester.Application.Content.Divisions.Queries.GetDivisionsQuery
{
    public class GetDivisionsQuery : IRequest<Page<DivisionDto>>
    {
        public int? PageIndex { get; set; } = null;
        public int? PageSize { get; set; } = null;
    }
}
