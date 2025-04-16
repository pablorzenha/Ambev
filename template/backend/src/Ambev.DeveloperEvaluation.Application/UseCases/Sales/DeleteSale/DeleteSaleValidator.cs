using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Sales.DeleteSale
{
    public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the DeleteSaleValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Unique identifier is required.
        /// </remarks>
        public DeleteSaleValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Sale Id is required");
        }
    }
}
