using Ambev.DeveloperEvaluation.Application.UseCases.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales.TestData
{
    public static class CreateSaleHandlerTestData
    {
        private static readonly Faker<CreateSaleCommand> SaleCommandFaker = new Faker<CreateSaleCommand>()
            .RuleFor(c => c.SaleNumber, f => $"VENDA{f.Random.AlphaNumeric(5)}")
            .RuleFor(c => c.Date, f => f.Date.Past())
            .RuleFor(c => c.Status, f => SaleStatus.NotCancelled)
            .RuleFor(c => c.CustomerId, f => Guid.NewGuid())
            .RuleFor(c => c.BranchId, f => Guid.NewGuid())
            .RuleFor(c => c.Items, f => new List<CreateSaleItemCommand>
            {
                    new CreateSaleItemCommand
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = f.Random.Number(1, 10),
                        UnitPrice = f.Finance.Amount(10, 100)
                    }
            });

        /// <summary>
        /// Generates a valid CreateSaleCommand.
        /// </summary>
        public static CreateSaleCommand GenerateValidCommand() => SaleCommandFaker.Generate();
    }
}
