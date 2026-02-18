using Azure.Core;
using MediatR;
using System.Diagnostics;
using TravelInspiration.API.Shared.Metrics;

namespace TravelInspiration.API.Shared.Behaviors;

public sealed class HandlerPerformanceMetricBehavior<TRequest, TResponse>(HandlerPerformanceMetric handlerPerformanceMetric) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly HandlerPerformanceMetric _handlerPerformanceMetric = handlerPerformanceMetric;
    private readonly Stopwatch _timer = new Stopwatch();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next();
        _timer.Stop();

        _handlerPerformanceMetric.AddMilliSecondsElapsed(_timer.ElapsedMilliseconds);

        return response;
    }
}
 