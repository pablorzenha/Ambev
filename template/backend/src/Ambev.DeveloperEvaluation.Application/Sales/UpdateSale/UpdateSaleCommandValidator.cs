using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.Dtos;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
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
    public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommandDto>
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
