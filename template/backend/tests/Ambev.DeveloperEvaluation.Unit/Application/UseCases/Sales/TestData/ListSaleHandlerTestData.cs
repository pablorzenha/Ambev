using Ambev.DeveloperEvaluation.Application.UseCases.Sales.ListSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales.TestData
{
    public static class ListSaleHandlerTestData
    {
        private static readonly Faker<ListSaleCommand> ListSaleCommandFaker = new Faker<ListSaleCommand>()
                .RuleFor(c => c.Skip, f => 1)
                .RuleFor(c => c.Take, f => 10)
                .RuleFor(c => c.Order, f => null)
                .RuleFor(c => c.TotalSize, f => 0);

        public static ListSaleCommand GenerateValidCommand() => ListSaleCommandFaker.Generate();
    }
}
