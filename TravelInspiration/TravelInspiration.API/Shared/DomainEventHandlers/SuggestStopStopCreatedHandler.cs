using MediatR;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Domain.Events;
using TravelInspiration.API.Shared.Persistence.Migrations;

namespace TravelInspiration.API.Shared.DomainEventHandlers;

public sealed class SuggestStopStopCreatedEventHandler(ILogger<SuggestStopStopCreatedEventHandler> logger, TravelInspirationDbContext dbContext)
    : INotificationHandler<StopCreatedEvent>
{
    private readonly ILogger<SuggestStopStopCreatedEventHandler> _logger = logger;
    private readonly TravelInspirationDbContext _dbContext = dbContext;

    public Task Handle(StopCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Listener {GetType().Name} to domain event {notification.GetType().Name}.");

        var incomingStop = notification.Stop;

        //AI is generating a new stop based on incomingStop

        Stop stopAIGenerated = new Stop($"Stop made by AI based on {incomingStop.Name}.")
        {
            ItineraryId = incomingStop.ItineraryId,
            ImageUri = new Uri("https://herebeimages.com/aigenerated.png"),
            IsSuggestedByAI = true
        };

        _dbContext.Stops.Add(stopAIGenerated);
        return Task.CompletedTask;
    }
}