using FluentValidation;
using MediatR;

namespace TravelInspiration.API.Shared.Behaviors;

public class ModelValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResult = _validators.Select(v => v.Validate(context)).ToList();

        var groupedValidationFailures = validationResult.SelectMany(v => v.Errors).GroupBy(e => e.PropertyName)
            .Select(g => new { PropertyName = g.Key, ValidationFailures = g.Select(v => new { v.ErrorMessage }) }).ToList();

        if (groupedValidationFailures.Count != 0)
        {
            var validationProblemsDictionary = new Dictionary<string, string[]>();
            foreach (var group in groupedValidationFailures)
            {
                var errorMessages = group.ValidationFailures.Select(v => v.ErrorMessage);
                validationProblemsDictionary.Add(group.PropertyName, errorMessages.ToArray());
            }

            return (TResponse)(object)Results.ValidationProblem(validationProblemsDictionary);
        }

        return await next();
    }
}
