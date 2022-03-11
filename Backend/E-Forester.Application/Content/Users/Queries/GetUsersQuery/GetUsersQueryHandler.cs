using AutoMapper;
using E_Forester.Application.CustomExceptions;
using E_Forester.Application.DataTransferObjects.Users;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Users.Queries.GetUsersQuery
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Page<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper, IAuthService authService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<Page<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var auth = _authService.GetCurrentUserRole() == UserRole.Admin;

            if (!auth)
                throw new ForbiddenException();

            var usersQuery = _userRepository.GetUsers();

            var users = new List<User>();

            if (request.PageSize > 0 && request.PageIndex > 0)
            {
                users = await SelectPage(usersQuery, (int)request.PageIndex, (int)request.PageSize);
            }
            else
            {
                users = await usersQuery
                    .OrderBy(u => u.Id)
                    .ToListAsync();
            }

            var userDtos = _mapper.Map<ICollection<User>, ICollection<UserDto>>(users);

            int total = usersQuery.Count();

            return new Page<UserDto>(userDtos, request.PageIndex, request.PageSize, total);
        }

        private async Task<List<User>> SelectPage(IQueryable<User> usersQuery, int pageIndex, int pageSize)
        {
            return await usersQuery
                    .OrderBy(u => u.Id)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Include(u => u.AssignedForestUnits)
                    .ToListAsync();
        }
    }
}

