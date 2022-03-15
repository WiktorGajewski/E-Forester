using AutoMapper;
using E_Forester.Application.DataTransferObjects.Users;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Infrastructure.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Account.Queries.GetProfileInfo
{
    public class GetProfileInfoQueryHandler : IRequestHandler<GetProfileInfoQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public GetProfileInfoQueryHandler(IUserRepository userRepository, IAuthService authService, IMapper mapper)
        {
            _userRepository = userRepository;
            this._authService = authService;
            this._mapper = mapper;
        }

        public async Task<UserDto> Handle(GetProfileInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = _authService.GetCurrentUserId();
            var user = await _userRepository.GetUserAsync(userId);

            var userDto = _mapper.Map<User, UserDto>(user);

            return userDto;
        }
    }
}
