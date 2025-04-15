using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the DeleteSaleRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Id must not be empty GUIDs
        /// </remarks>
        public DeleteSaleRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty()
               .WithMessage("Sale ID is required");
        }
    }
}
