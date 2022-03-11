using E_Forester.Application.DataTransferObjects.Users;
using MediatR;

namespace E_Forester.Application.Content.Account.Queries.GetProfileInfo
{
    public class GetProfileInfoQuery : IRequest<UserDto>
    {
    }
}
