using AutoMapper;
using E_Forester.Application.DataTransferObjects.Users;
using E_Forester.Application.Pagination.Wrappers;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
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

        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Page<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
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
