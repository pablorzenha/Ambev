using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public class ListSaleRequestValidator : AbstractValidator<ListSaleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the ListSaleRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Skip: Must be greater than or equal to 0
        /// - Take: Must be less than or equal to 25
        /// </remarks>
        public ListSaleRequestValidator()
        {
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0)
            .WithMessage("O valor de Skip não pode ser menor que 0.");
            RuleFor(x => x.Take).LessThanOrEqualTo(25)
            .WithMessage("O valor máximo de Take deve ser 25.");
        }
    }
}