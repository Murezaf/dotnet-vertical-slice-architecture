using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Events;
using TravelInspiration.API.Shared.Persistence.Migrations;
using TravelInspiration.API.Shared.Security;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Stops;

public sealed class UpdateStop : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPut("api/itineraries/{itineraryId}/stops/{stopId}", (int itineraryId, int stopId,
            UpdateStopCommand updateStopCommand, IMediator mediator, CancellationToken cancellationToken) =>
            {
                updateStopCommand.ItineraryId = itineraryId;
                updateStopCommand.StopId = stopId;

                return mediator.Send(updateStopCommand, cancellationToken);
            }).RequireAuthorization(AuthorizationPolicies.HasWriteActionPolicy);
    }
}

public sealed class UpdateStopCommand : IRequest<IResult>
{
    public int ItineraryId { get; set; }
    public int StopId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageUri { get; set; }
    public bool? IsSuggestedByAI { get; set; } //We may want to update stop from suggested by AI to a real stop
}

public sealed class UpdateStopCommandHandler(TravelInspirationDbContext dbContext, IMapper mapper) 
    : IRequestHandler<UpdateStopCommand, IResult>
{
    private readonly TravelInspirationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<IResult> Handle(UpdateStopCommand request, CancellationToken cancellationToken)
    {
        var stopToUpdate = await _dbContext.Stops.FirstOrDefaultAsync(s => s.Id == request.StopId && s.ItineraryId == request.ItineraryId, cancellationToken);

        if (stopToUpdate == null)
            return Results.NotFound();

        stopToUpdate.HandleUpdateCommand(request);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Results.Ok(_mapper.Map<StopDto>(stopToUpdate));
    }
}

public sealed class UpdateStopCommandValidator : AbstractValidator<UpdateStopCommand>
{
    public UpdateStopCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotEmpty();
        RuleFor(v => v.ImageUri).Must(ImageUri => Uri.TryCreate(ImageUri ?? "", UriKind.Absolute, out var imageUri))
            .When(v => !string.IsNullOrWhiteSpace(v.ImageUri))
            .WithMessage("ImageUri must be valid");
    }
}

public sealed class SuggestStopStopUpdatedEventHandel(ILogger<SuggestStopStopUpdatedEventHandel> logger) 
    : INotificationHandler<StopUpdatedEvent>
{
    private readonly ILogger<SuggestStopStopUpdatedEventHandel> _logger = logger; 

    public Task Handle(StopUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Listener {GetType().Name} to domain event {notification.GetType().Name}.");

        //AI things is happening

        return Task.CompletedTask;
    }
}