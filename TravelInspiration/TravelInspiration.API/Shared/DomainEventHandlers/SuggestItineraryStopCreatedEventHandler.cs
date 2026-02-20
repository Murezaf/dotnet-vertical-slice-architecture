using MediatR;
using TravelInspiration.API.Shared.Domain.Events;

namespace TravelInspiration.API.Shared.DomainEventHandlers;

public sealed class SuggestItineraryStopCreatedEventHandler(ILogger<SuggestItineraryStopCreatedEventHandler> logger)
    : INotificationHandler<StopCreatedEvent>
{
    private readonly ILogger _logger = logger;

    public Task Handle(StopCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Listener {GetType().Name} to domain event {notification.GetType().Name}.");

        //AI things is happening

        return Task.CompletedTask;
    }
}

