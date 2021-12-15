using E_Forester.Application.Security.Interfaces;
using E_Forester.Data.Interfaces;
using E_Forester.Model.Database;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.PlanExecutions.Commands.CreatePlanExecution
{
    public class CreatePlanExecutionCommandHandler : IRequestHandler<CreatePlanExecutionCommand>
    {
        private readonly IPlanExecutionRepository _planExecutionRepository;
        private readonly IAuthService _authService;

        public CreatePlanExecutionCommandHandler(IPlanExecutionRepository planExecutionRepository, IAuthService authService)
        {
            _planExecutionRepository = planExecutionRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(CreatePlanExecutionCommand request, CancellationToken cancellationToken)
        {
            var planExecution = new PlanExecution()
            {
                Quantity = request.Quantity,
                CreatedAt = DateTime.UtcNow,
                PlanItemId = request.PlanItemId,
                PlanId = request.PlanId,
                CreatorId = _authService.GetCurrentUserId()
            };

            await _planExecutionRepository.CreatePlanExecutionAsync(planExecution);

            return await Task.FromResult(Unit.Value);
        }
    }
}
