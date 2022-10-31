using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PracticalTest.Infrastructure.Blogs.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse: IResult
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IValidator<TRequest> _validator;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IValidator<TRequest> validator)
    {
        _logger = logger;
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
        RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation($"Handling {typeof(TRequest).Name}"); 
        
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .Select(validationFailure => validationFailure.ErrorMessage).ToList();
        _logger.LogInformation($"Handled {typeof(TResponse).Name}"); 

        return (dynamic)errors;
            
   
    }
}