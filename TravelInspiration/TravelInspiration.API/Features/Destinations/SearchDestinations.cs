using Microsoft.AspNetCore.Http.HttpResults;
using TravelInspiration.API.Shared.Networking;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Destinations;

//public static class SearchDestinations
public sealed class SearchDestinations : ISlice
{
    //public static void AddEndpoint(IEndpointRouteBuilder app)
    public void AddEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/destinations", async (string? searchFor, ILoggerFactory logger,
            IDestinationSearchApiClient destinationSearchApiClient, CancellationToken cancellationToken) =>
        {
            logger.CreateLogger("Endpoint Handler").LogInformation("SearchDestinations feature called.");

            var resultFromExternalApiCall = await destinationSearchApiClient.GetDestinationsAsync(searchFor, cancellationToken);

            var result = resultFromExternalApiCall.Select(r => new { r.Name, r.Description, r.ImageUri });

            return Results.Ok(result);
        });
    }
}
