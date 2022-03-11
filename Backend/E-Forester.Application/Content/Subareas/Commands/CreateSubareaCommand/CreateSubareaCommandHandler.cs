using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using E_Forester.Model.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Subareas.Commands.CreateSubareaCommand
{
    public class CreateSubareaCommandHandler : IRequestHandler<CreateSubareaCommand>
    {
        private readonly ISubareaRepository _subareaRepository;
        private readonly IAuthService _authService;

        public CreateSubareaCommandHandler(ISubareaRepository subareaRepository, IAuthService authService)
        {
            _subareaRepository = subareaRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(CreateSubareaCommand request, CancellationToken cancellationToken)
        {
            var auth = _authService.GetCurrentUserRole() == UserRole.Admin;

            if (!auth)
                throw new ForbiddenException();

            var subarea = new Subarea()
            {
                Address = request.Address,
                Area = request.Area,
                DivisionId = request.DivisionId
            };

            await _subareaRepository.CreateSubareaAsync(subarea);

            return await Task.FromResult(Unit.Value);
        }
    }
}
