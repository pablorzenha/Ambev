using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Sales.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateSaleValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - ID: Unique identifier is required.
        /// - Date: Date must not be in the future.
        /// - SaleNumber: SaleNumber is required.
        /// - CustomerId: CustomerId must not be empty GUIDs
        /// - BranchId: BranchId must not be empty GUIDs
        /// - Items: At least one item is required
        /// - Status: Status must be Cancelled or NotCancelled
        /// </remarks>
        public UpdateSaleValidator()
        {
            RuleFor(sale => sale.Id).NotNull();

            RuleFor(sale => sale.Date)
                 .NotEmpty().WithMessage("The sale date is required.")
                 .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The sale date cannot be in the future.");

            RuleFor(sale => sale.SaleNumber).NotEmpty().Length(10, 50);

            RuleFor(sale => sale.CustomerId)
                    .NotEqual(Guid.Empty).WithMessage("CustomerId is required.");

            RuleFor(sale => sale.BranchId)
                    .NotEqual(Guid.Empty).WithMessage("BranchId is required.");

            RuleFor(sale => sale.Items)
                    .NotNull().WithMessage("Sale must contain at least one item.")
                    .Must(items => items.Count > 0).WithMessage("Sale must contain at least one item.");

            RuleFor(sale => sale.Status)
                    .NotEqual(SaleStatus.None);

            RuleForEach(sale => sale.Items)
                    .SetValidator(new UpdateSaleItemCommandValidator());

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
    /// </remarks>
    public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        public UpdateSaleItemCommandValidator()
        {
            RuleFor(item => item.ProductId)
                .NotEqual(Guid.Empty).WithMessage("ProductId is required.");

            RuleFor(item => item.Quantity)
                .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

        }
    }
}
