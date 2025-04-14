using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public class ListSaleRequestValidator : AbstractValidator<ListSaleRequest>
    {
        /// <summary>
        /// Initializes validation rules for GetSaleRequest
        /// </summary>
        public ListSaleRequestValidator()
        {
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0)
            .WithMessage("O valor de Skip não pode ser menor que 0.");
            RuleFor(x => x.Take).LessThanOrEqualTo(25)
            .WithMessage("O valor máximo de Take deve ser 25.");
        }
    }
}