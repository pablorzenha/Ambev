using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Dtos;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
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

            RuleFor(item => item.Status)
                .NotEqual(SaleStatus.None);

            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemRequestValidator());
        }
    }
    public class SaleItemRequestValidator : AbstractValidator<CreateSaleItemRequestDto>
    {
        public SaleItemRequestValidator()
        {
            RuleFor(item => item.ProductId)
                .NotEqual(Guid.Empty).WithMessage("ProductId is required.");

            RuleFor(item => item.Quantity)
                .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0.");


            //RuleFor(item => item.Discount)
            //    .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.")
            //    .Must((item, discount) =>
            //    {
            //        if (item.Quantity < 4 && discount > 0) return false;
            //        if (item.Quantity >= 4 && item.Quantity < 10) return discount <= 0.10m * item.UnitPrice * item.Quantity;
            //        if (item.Quantity >= 10 && item.Quantity <= 20) return discount <= 0.20m * item.UnitPrice * item.Quantity;
            //        return true;
            //    }).WithMessage("Discount does not match the allowed discount rules based on quantity.");


        }
    }
}
