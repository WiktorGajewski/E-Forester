using E_Forester.Application.CustomExceptions;
using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Plans.Commands.ClosePlanCommand
{
    public class ClosePlanCommandHandler : IRequestHandler<ClosePlanCommand>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IAuthService _authService;

        public ClosePlanCommandHandler(IPlanRepository planRepository, IAuthService authService)
        {
            _planRepository = planRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(ClosePlanCommand request, CancellationToken cancellationToken)
        {
            var auth = _authService.GetCurrentUserRole() == UserRole.Admin;

            if (!auth)
                throw new ForbiddenException();

            var plan = await _planRepository.GetPlanAsync(request.Id);

            if (plan == null)
                throw new NotFoundException();

            await _planRepository.ClosePlanAsync(plan);

            return await Task.FromResult(Unit.Value);
        }
    }
}
