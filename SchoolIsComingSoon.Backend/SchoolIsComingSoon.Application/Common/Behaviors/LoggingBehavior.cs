using MediatR;
using SchoolIsComingSoon.Application.Interfaces;
using Serilog;

namespace SchoolIsComingSoon.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest
        : IRequest<TResponse>
    {
        ICurrentUserService _currentUserService;

        public LoggingBehavior(ICurrentUserService currentUserService) =>
            _currentUserService = currentUserService;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId;

            Log.Information("HBM Request: {Name} {@UserId} {@Request}",
                requestName, userId, request);

            var response = await next();

            return response;
        }
    }
}