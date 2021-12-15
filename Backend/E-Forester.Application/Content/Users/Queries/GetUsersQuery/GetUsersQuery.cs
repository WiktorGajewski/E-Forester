using E_Forester.Application.DataTransferObjects.Users;
using MediatR;
using System.Collections.Generic;

namespace E_Forester.Application.Content.Users.Queries.GetUsersQuery
{
    public class GetUsersQuery : IRequest<ICollection<UserDto>>
    {

    }
}
