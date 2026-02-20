using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Itineraries;

public class CreateItineraryWithStops : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        //Not implemented yet

        /*What important here is that in this slice we are creating new stops exactly like CreateStop slice. 
        so we need event handlers in our CreateStop slice, but we can't have direct dependency between two slices
        these event handlers are not slice specific anymore. So we should put them in a shared namespace*/
    }
}
