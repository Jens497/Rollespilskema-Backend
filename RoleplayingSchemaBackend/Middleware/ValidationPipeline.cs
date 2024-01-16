using FluentValidation;
using MediatR;
using RoleplayingSchemaBackend.Handlers.Interface;
using ValidationException = RoleplayingSchemaBackend.Exceptions.ValidationExceptionPipeline;

namespace RoleplayingSchemaBackend.Middleware
{
    public sealed class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, ICommand<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators; // This is a list that holds all the errors that comes from the validators

        public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators) 
            => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next(); // If there are no validation errors return and wait for the next Command request
            }

            var context = new ValidationContext<TRequest>(request);
            //The following selection from EF can by done in more ways, this was just the most generic way to collect all the validations errors that are found.
            var errorsDict = _validators.Select(v => v.Validate(context))
                .SelectMany(v => v.Errors)
                .Where(v => v != null)
                .GroupBy(v => v.PropertyName, v => v.ErrorMessage,
                (propertyName, errorMessage) => new
                {
                    Key = propertyName,
                    Value = errorMessage.First()
                }).ToDictionary(d => d.Key, v => v.Value);

            //Check if erros and throw if there are any
            //Dictionary<string, (string, string[])> newDict;
            //newDict.Add("ValidationError", errorsDict);

            //newDict.Add("ValidationErrors", errorsDict);
            if (errorsDict.Any())
            {
                throw new ValidationException(errorsDict);
            }

            return await next(); //After the errors has been thrown return and await for the next command coming.
        }
    }
}
