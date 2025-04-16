using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class SaleItemTestData
    {

        private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
            .RuleFor(si => si.Quantity, f => f.Random.Int(1, 20))
            .RuleFor(si => si.UnitPrice, f => f.Random.Decimal(0.1m, 100))
            .RuleFor(si => si.ProductId, f => Guid.NewGuid())
            .RuleFor(si => si.SaleId, f => Guid.NewGuid());


        /// <summary>
        /// Generates a valid Sale Item entity with randomized data.
        /// </summary>
        public static SaleItem GenerateValidSaleItem()
        {
            return SaleItemFaker.Generate();
        }

        /// <summary>
        /// Generates a valid quantity using Faker.
        /// </summary>
        public static int GenerateValidQuantity()
        {
            return new Faker().Random.Int(1, 20);
        }

        /// <summary>
        /// Generates a valid unit price using Faker.
        /// </summary>
        public static decimal GenerateValidUnitPrice()
        {
            return Math.Round(new Faker().Random.Decimal(0.01m, 1000.0m), 2);
        }

        /// <summary>
        /// Generates a valid unique identifier of the procuct using Faker.
        /// </summary>
        public static Guid GenerateProductId()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Generates a valid unique identifier of the sale using Faker.
        /// </summary>
        public static Guid GenerateSaleId()
        {
            return Guid.NewGuid();
        }
    }
}
