using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence.Migrations;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Itineraries;

//public static class GetItineraries
//{
//    public static void AddEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapGet("api/itineraries", async (string? searchFor, TravelInspirationDbContext dbContext, IMapper mapper,
//            ILoggerFactory logger, CancellationToken cancellationToken) =>
//        {
//            logger.CreateLogger("Endpoint Handler").LogInformation("GetItineraries feature called");

//            var result = await dbContext.Itineraries.Where(i => 
//                searchFor == null || i.Name.Contains(searchFor) || (i.Description != null && i.Description.Contains(searchFor)))
//                .ToListAsync(cancellationToken);

//            return Results.Ok(mapper.Map<IEnumerable<ItineraryDto>>(result));
//        });
//    }
//}

public sealed class GetItinerariesQuery(string? searchFor) : IRequest<IResult>//IRequest<GetItinerariesResponse>
{
    public string? SearchFor { get; } = searchFor;
}

//public sealed class GetItinerariesResponse(IEnumerable<ItineraryDto> itineraries)
//{ 
//    public IEnumerable<ItineraryDto> Itineraries { get; } = itineraries;
//}

public sealed class GetItinerariesHandler(TravelInspirationDbContext dbContext, IMapper mapper) : IRequestHandler<GetItinerariesQuery, IResult>//IRequestHandler<GetItinerariesQuery, GetItinerariesResponse>
{
    private readonly IMapper _mapper = mapper;
    private readonly TravelInspirationDbContext _dbContext = dbContext;

    //public async Task<GetItinerariesResponse> Handle(GetItinerariesQuery request, CancellationToken cancellationToken)
    public async Task<IResult> Handle(GetItinerariesQuery request, CancellationToken cancellationToken)
    {
        var itineraries = await _dbContext.Itineraries.Where(i =>
            request.SearchFor == null || i.Name.Contains(request.SearchFor) || (i.Description != null && i.Description.Contains(request.SearchFor)))
            .ToListAsync(cancellationToken);

        var mappedItineraries = _mapper.Map<IEnumerable<ItineraryDto>>(itineraries);

        //return new GetItinerariesResponse(mappedItineraries);
        return Results.Ok(mappedItineraries);
    }
}

//public static class GetItineraries
public sealed class GetItineraries : ISlice
{
    //public static void AddEndpoint(IEndpointRouteBuilder app)
    public void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/itineraries", (string? searchFor, IMediator mediator, 
            //ILoggerFactory logger,
            CancellationToken cancellationToken) =>
        {
            //logger.CreateLogger("Endpoint Handler").LogInformation("GetItineraries feature called");

            return mediator.Send(new GetItinerariesQuery(searchFor), cancellationToken);
        });
    }
}

public sealed class ItineraryDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string UserId { get; set; }
}

public sealed class ItineraryMapProfile : Profile
{
    public ItineraryMapProfile()
    {
        CreateMap<Itinerary, ItineraryDto>();
    }
}
