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
        }
    }
}