using AutoMapper;
using E_Forester.Application.DataTransferObjects.Users;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Users.Queries.GetUsersQuery
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ICollection<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync();
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(users);
        }
    }
}
