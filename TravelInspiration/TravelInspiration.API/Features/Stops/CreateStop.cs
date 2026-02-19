using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence.Migrations;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Stops;

public sealed class CreateStop : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("api/itineraries/{itineraryId}/stops", (int itineraryId, CreateStopCommand createStopCommand,
            IMediator mediator, CancellationToken cancellationToken) =>
            {
                createStopCommand.ItineraryId = itineraryId;
                return mediator.Send(createStopCommand);
            });
    }
}

public sealed class CreateStopCommand(int itineraryId, string name, string? imageUri) : IRequest<IResult>
{
    public int ItineraryId { get; set; } = itineraryId;
    public string Name { get; } = name;
    public string? ImageUri { get; set; } = imageUri;
}

public sealed class CreateStopCommandHandler(TravelInspirationDbContext dbContext, IMapper mapper) : IRequestHandler<CreateStopCommand, IResult>
{
    private readonly TravelInspirationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<IResult> Handle(CreateStopCommand request, CancellationToken cancellationToken)
    {
        if (!await _dbContext.Itineraries.AnyAsync(i => i.Id == request.ItineraryId))
            return Results.NotFound();

        var newStopEntity = new Stop(request.Name);
        newStopEntity.HandleCreateCommand(request);

        _dbContext.Stops.Add(newStopEntity);
        await _dbContext.SaveChangesAsync();

        return Results.Created($"api/itineraries/{newStopEntity.ItineraryId}/stops/{newStopEntity.Id}" , _mapper.Map<StopDto>(newStopEntity));
    }
}

//public sealed class StopDto
//{
//    public required int Id { get; set; }
//    public required string Name { get; set; }
//    public Uri? ImageUri { get; set; }
//    public required int ItineraryId { get; set; }
//}

//public sealed class StopMapProfileAfterCreation : Profile
//{

//}

public sealed class CreateStopCommandValidator : AbstractValidator<CreateStopCommand>
{
    public CreateStopCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotEmpty();
        RuleFor(v => v.ImageUri).Must(ImageUri => Uri.TryCreate(ImageUri ?? "", UriKind.Absolute, out var imageUri))
            .When(v => !string.IsNullOrWhiteSpace(v.ImageUri))
            .WithMessage("ImageUri must be valid");
    }
}