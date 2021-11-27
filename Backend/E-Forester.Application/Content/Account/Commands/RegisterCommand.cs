using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Account.Commands
{
    public class RegisterCommand : IRequest
    {
        internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand>
        {
            public RegisterCommandHandler()
            {

            }

            public Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
