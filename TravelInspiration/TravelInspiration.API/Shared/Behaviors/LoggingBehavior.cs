using MediatR.Pipeline;
using TravelInspiration.API.Shared.Security;

namespace TravelInspiration.API.Shared.Behaviors;

public sealed class LoggingBehavior<TRequest>(ILogger<TRequest> logger, ICurrentUserService currentUserService) 
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger = logger;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Starting feature execution: {typeof(TRequest).Name}, user {_currentUserService.UserId}.");

        return Task.CompletedTask;
    }
}
