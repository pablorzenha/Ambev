using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.Date)
                .NotEmpty().WithMessage("The sale date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The sale date cannot be in the future.");

            RuleFor(sale => sale.CustomerId)
                .NotEqual(Guid.Empty).WithMessage("CustomerId is required.");

            RuleFor(sale => sale.BranchId)
                .NotEqual(Guid.Empty).WithMessage("BranchId is required.");

            RuleFor(sale => sale.Items)
                .NotEmpty().WithMessage("Sale must contain at least one item.");

            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemDtoValidator());
        }

        public class SaleItemDtoValidator : AbstractValidator<SaleItem>
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
}
