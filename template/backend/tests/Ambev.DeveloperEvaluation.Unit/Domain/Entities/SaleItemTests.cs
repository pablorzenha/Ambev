using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleItemTests
    {
        private readonly Faker _faker = new();

        [Fact]
        public void Constructor_Should_Set_Values_Correctly_When_Valid_Parameters_Are_Passed()
        {
            var quantity = 5;
            var unitPrice = 10.0m;
            var productId = Guid.NewGuid();
            var saleId = Guid.NewGuid();

            var item = new SaleItem(quantity, unitPrice, productId, saleId);

            Assert.Equal(quantity, item.Quantity);
            Assert.Equal(unitPrice, item.UnitPrice);
            Assert.Equal(productId, item.ProductId);
            Assert.Equal(saleId, item.SaleId);
            Assert.Equal(0.10m, item.Discount);
            Assert.Equal((quantity * unitPrice) * 0.90m, item.TotalPrice);
        }

        [Fact]
        public void Set_Quantity_Should_Recalculate_Total_Price()
        {
            var item = new SaleItem(2, 20, Guid.NewGuid(), Guid.NewGuid());
            var originalUpdatedAt = item.UpdatedAt;

            item.SetQuantity(10);

            Assert.Equal(0.20m, item.Discount);
            Assert.Equal((10 * 20) * (1- item.Discount), item.TotalPrice);
            Assert.NotEqual(originalUpdatedAt, item.UpdatedAt);
        }

        [Fact]
        public void Set_Same_Quantity_Should_Not_Recalculate_Total_Price()
        {
            var item = new SaleItem(2, 20, Guid.NewGuid(), Guid.NewGuid());

            var originalUpdatedAt = item.UpdatedAt;
            item.SetQuantity(2);

            Assert.Equal(originalUpdatedAt, item.UpdatedAt);
        }

        [Fact]
        public void Set_Unit_Price_Should_Recalculate_Total_Price()
        {
            var item = new SaleItem(2, 20, Guid.NewGuid(), Guid.NewGuid());

            var originalUpdatedAt = item.UpdatedAt;
            item.SetUnitPrice(2);

            Assert.True(originalUpdatedAt != item.UpdatedAt);
        }

        [Fact]
        public void Set_Same_Unit_Price_Should_Not_Recalculate_Total_Price()
        {
            var item = new SaleItem(2, 20, Guid.NewGuid(), Guid.NewGuid());

            var originalUpdatedAt = item.UpdatedAt;
            item.SetUnitPrice(20);

            Assert.Equal(originalUpdatedAt, item.UpdatedAt);
        }

        [Fact]
        public void Update_Should_Throw_When_Quantity_Greater_Than_20()
        {
            var item = new SaleItem(2, 15, Guid.NewGuid(), Guid.NewGuid());
            var productId = Guid.NewGuid();

            var ex = Assert.Throws<InvalidOperationException>(() =>
                item.Update(productId, 21, 15)
            );

            Assert.Contains("exceeds the 20 items limit", ex.Message);
        }
    }
}
