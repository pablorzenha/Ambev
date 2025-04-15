using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Validation
{
    public static class RequestValidator
    {
        public static async Task ValidateAsync<TCommand>(TCommand command, AbstractValidator<TCommand> validator, CancellationToken cancellationToken = default)
        {
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
