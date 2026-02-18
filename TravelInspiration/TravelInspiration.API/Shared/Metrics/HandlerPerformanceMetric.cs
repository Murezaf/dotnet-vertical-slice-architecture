using System.Diagnostics.Metrics;

namespace TravelInspiration.API.Shared.Metrics;

public sealed class HandlerPerformanceMetric
{
    private readonly Counter<long> _milliSecondsElapsed;
    public HandlerPerformanceMetric(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("TravelInspiration.API");
        _milliSecondsElapsed = meter.CreateCounter<long>("travelinspiration.api.requesthandler.millisecondselapsed");
    }

    public void AddMilliSecondsElapsed(long millisecondsElapsed) => _milliSecondsElapsed.Add(millisecondsElapsed);    
}
