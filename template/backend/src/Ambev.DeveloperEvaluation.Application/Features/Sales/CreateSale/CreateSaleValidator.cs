using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Date: Date must not be in the future.
        /// - SaleNumber: SaleNumber is required.
        /// - CustomerId: CustomerId must not be empty GUIDs
        /// - BranchId: BranchId must not be empty GUIDs
        /// - Items: At least one item is required
        /// - Status: Status must be Cancelled or NotCancelled
        /// </remarks>
        public CreateSaleValidator()
        {
            RuleFor(sale => sale.Date)
                .NotEmpty().WithMessage("The sale date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The sale date cannot be in the future.");

            RuleFor(sale => sale.SaleNumber)
                .NotEmpty().WithMessage("Sale Number is required.")
                .Length(10, 50).WithMessage("Sale Number is more than 10 digits.");

            RuleFor(sale => sale.CustomerId)
                .NotEqual(Guid.Empty).WithMessage("CustomerId is required.");

            RuleFor(sale => sale.BranchId)
                .NotEqual(Guid.Empty).WithMessage("BranchId is required.");

            RuleFor(sale => sale.Items)
                .NotEmpty().WithMessage("Sale must contain at least one item.")
                .Must(items => items.Count > 0).WithMessage("Sale must contain at least one item.");

            RuleFor(sale => sale.Status)
                .NotEqual(SaleStatus.None)
                .Must(status => status == SaleStatus.Cancelled || status == SaleStatus.NotCancelled)
                .WithMessage("Status must be either 'Cancelled' or 'NotCancelled'.");

            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemDtoValidator());
        }
    }

    /// <summary>
    /// Initializes a new instance of the SaleItemDtoValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductId: ProductId must not be empty
    /// - Quantity: Quantity must be between 1 and 20
    /// - UnitPrice: UnitPrice must be greater than 0
    /// - Discount: Discount must be non-negative and respect limits based on quantity
    /// </remarks>
    public class SaleItemDtoValidator : AbstractValidator<CreateSaleItemCommand>
    {
        public SaleItemDtoValidator()
        {
            RuleFor(item => item.ProductId)
                .NotEqual(Guid.Empty).WithMessage("ProductId is required.");

            RuleFor(item => item.Quantity)
                .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

            RuleFor(item => item.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.")
                .Must((item, discount) =>
                {
                    if (item.Quantity < 4 && discount > 0) return false;
                    if (item.Quantity >= 4 && item.Quantity < 10) return discount <= 0.10m * item.UnitPrice * item.Quantity;
                    if (item.Quantity >= 10 && item.Quantity <= 20) return discount <= 0.20m * item.UnitPrice * item.Quantity;
                    return true;
                }).WithMessage("Discount is not valid for the quantity range.");
        }
    }
}
