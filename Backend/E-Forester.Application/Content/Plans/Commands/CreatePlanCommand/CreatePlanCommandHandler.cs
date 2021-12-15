using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Plans.Commands.CreatePlanCommand
{
    public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IAuthService _authService;

        public CreatePlanCommandHandler(IPlanRepository planRepository, IAuthService authService)
        {
            _planRepository = planRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = new Plan()
            {
                Year = request.Year,
                CreatedAt = DateTime.UtcNow,
                ForestUnitId = request.ForestUnitId,
                CreatorId = _authService.GetCurrentUserId()
            };

            await _planRepository.CreatePlanAsync(plan);

            return await Task.FromResult(Unit.Value);
        }
    }
}