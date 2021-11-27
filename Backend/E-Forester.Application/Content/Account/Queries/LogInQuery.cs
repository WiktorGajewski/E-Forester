using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E_Forester.Application.Content.Account.Queries
{
    public class LogInQuery : IRequest<string>
    {
        internal sealed class LogInQueryHandler : IRequestHandler<LogInQuery, string>
        {
            public LogInQueryHandler()
            {

            }

            public Task<string> Handle(LogInQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
