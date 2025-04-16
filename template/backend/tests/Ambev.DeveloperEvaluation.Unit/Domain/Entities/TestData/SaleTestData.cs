using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class SaleTestData
    {
        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
            .RuleFor(s => s.SaleNumber, f => $"VENDA{f.Random.AlphaNumeric(5)}")
            .RuleFor(s => s.CustomerId, Guid.NewGuid())
            .RuleFor(s => s.BranchId, Guid.NewGuid())
            .RuleFor(s => s.Date, f => f.Date.Past())
            .RuleFor(s => s.Status, SaleStatus.NotCancelled)
            .RuleFor(s => s.Items, f=> new List<SaleItem>() );

        /// <summary>
        /// Generates a valid Sale entity with randomized data.
        /// </summary>
        public static Sale GenerateValidSale()
        {
            return SaleFaker.Generate();
        }

        /// <summary>
        /// Generates a valid sale number using Faker.
        /// </summary>
        public static string GenerateValidSaleNumber()
        {
            return $"VENDA{new Faker().Random.AlphaNumeric(5)}";
        }

        /// <summary>
        /// Generates a valid Date using Faker.
        /// </summary>
        public static DateTime GenerateValidPastDate()
        {
            return new Faker().Date.Past();
        }

        /// <summary>
        /// Generates a valid customer using Faker.
        /// </summary>
        public static Guid GenerateCustomerId()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Generates a valid branch using Faker.
        /// </summary>
        public static Guid GenerateBranchId()
        {
            return Guid.NewGuid();
        }
    }
}
