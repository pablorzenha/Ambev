﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Validation
{
    public static class RequestValidation
    {
        public static async Task ValidateAsync<TCommand>(TCommand command, AbstractValidator<TCommand> validator, CancellationToken cancellationToken = default)
        {
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
