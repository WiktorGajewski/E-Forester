using E_Forester.Application.DataTransferObjects.Users;
using E_Forester.Application.Pagination.Wrappers;
using MediatR;

namespace E_Forester.Application.Content.Users.Queries.GetUsersQuery
{
    public class GetUsersQuery : IRequest<Page<UserDto>>
    {
        public int? PageIndex { get; set; } = null;
        public int? PageSize { get; set; } = null;
    }
}
