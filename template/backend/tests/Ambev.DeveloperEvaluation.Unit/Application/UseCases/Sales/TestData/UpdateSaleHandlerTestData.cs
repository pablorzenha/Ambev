using Ambev.DeveloperEvaluation.Application.UseCases.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales.TestData
{
    public static class UpdateSaleHandlerTestData
    {

        private static readonly Faker<UpdateSaleCommand> GetSaleCommandFaker = new Faker<UpdateSaleCommand>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.SaleNumber, f => $"VENDA{f.Random.AlphaNumeric(5)}")
            .RuleFor(c => c.Date, f => f.Date.Past())
            .RuleFor(c => c.Status, f => SaleStatus.NotCancelled)
            .RuleFor(c => c.CustomerId, f => Guid.NewGuid())
            .RuleFor(c => c.BranchId, f => Guid.NewGuid())
            .RuleFor(c => c.Items, f => new List<UpdateSaleItemCommand>
            {
                    new UpdateSaleItemCommand
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = f.Random.Number(1, 10),
                        UnitPrice = f.Finance.Amount(10, 100)
                    }
            });
        /// <summary>
        /// Generates a valid UpdateSaleCommand.
        /// </summary>
        public static UpdateSaleCommand GenerateValidCommand() => GetSaleCommandFaker.Generate();
    }
}
