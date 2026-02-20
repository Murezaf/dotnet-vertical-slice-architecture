using TravelInspiration.API.Shared.Domain.Entities;

namespace TravelInspiration.API.Shared.Domain.Events;

public class StopUpdatedEvent(Stop stop) : DomainEvent
{
    Stop Stop { get; set; } = stop;
}
