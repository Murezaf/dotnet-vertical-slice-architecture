using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence.Migrations;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Stops;

//public static class GetStops
//{
//    public static void AddEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapGet("api/itineraries/{itineraryId}/stops", async(int itineraryId, TravelInspirationDbContext dbContext,
//            ILoggerFactory logger, IMapper mapper, CancellationToken cancellationToken) =>
//        {
//            logger.CreateLogger("Endpoint Handler").LogInformation("GetStops feature called");

//            var itinerary = await dbContext.Itineraries.Include(i => i.Stops).FirstOrDefaultAsync(i => i.Id == itineraryId, cancellationToken);

//            if (itinerary == null)
//                return Results.NotFound();

//            return Results.Ok(mapper.Map<IEnumerable<StopDto>>(itinerary.Stops));
//        });
//    }
//}

public sealed class GetStopsQuery(int itineraryId) : IRequest<IResult>
{
    public int ItineraryId { get; } = itineraryId;
}

public sealed class GetStopsHandler(TravelInspirationDbContext dbContext, IMapper mapper) : IRequestHandler<GetStopsQuery, IResult>
{
    private readonly TravelInspirationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<IResult> Handle(GetStopsQuery request, CancellationToken cancellationToken)
    {
        var itinerary = await _dbContext.Itineraries.Include(i => i.Stops).AsNoTracking().FirstOrDefaultAsync(i => i.Id == request.ItineraryId, cancellationToken);

        if (itinerary == null)
            return Results.NotFound();

        return Results.Ok(_mapper.Map<IEnumerable<StopDto>>(itinerary.Stops));
    }
}

//public static class GetStops
public sealed class GetStops : ISlice
{
    //public static void AddEndpoint(IEndpointRouteBuilder app)
    public void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/itineraries/{itineraryId}/stops", (int itineraryId, IMediator mediator,
            //ILoggerFactory logger,
            CancellationToken cancellationToken) =>
        {
            //logger.CreateLogger("Endpoint Handler").LogInformation("GetStops feature called");

            return mediator.Send(new GetStopsQuery(itineraryId), cancellationToken);
        }).RequireAuthorization();
    }
}

public sealed class StopDto
{
    public required int Id { get; set; }
    public required string Name {  get; set; }
    public Uri? ImageUri {  get; set; }
    public required int ItineraryId { get; set; }
    public bool? IsSuggestedByAI { get; set; }
}

public sealed class StopMapProfile : Profile
{
    public StopMapProfile()
    {
        CreateMap<Stop, StopDto>();
    }
}
