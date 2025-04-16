using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Domain.Entities
{
    public class SaleItemTests
    {

        [Fact]
        public void Constructor_Should_Calculate_Total_Price()
        {
            var unitPrice = 10.0m;
            var productId = Guid.NewGuid();
            var saleId = Guid.NewGuid();
            var quantity1 = 10;

            var item1 = new SaleItem(quantity1, unitPrice, productId, saleId);

            Assert.Equal(quantity1, item1.Quantity);
            Assert.Equal(0.2m, item1.Discount);
            Assert.Equal(item1.Quantity * item1.UnitPrice * (1 - item1.Discount), item1.TotalPrice);
        }

        [Fact]
        public void Should_Create_SaleItem_Without_Discount()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var saleId = Guid.NewGuid();
            int quantity = 2;
            decimal unitPrice = 10m;

            // Act
            var item = new SaleItem(quantity, unitPrice, productId, saleId);

            // Assert
            Assert.Equal(quantity, item.Quantity);
            Assert.Equal(unitPrice, item.UnitPrice);
            Assert.Equal(0, item.Discount);
            Assert.Equal(20m, item.TotalPrice); // 2 * 10
        }

        [Fact]
        public void Should_Create_SaleItem_And_Apply_Discount()
        {
            var productId = Guid.NewGuid();
            var saleId = Guid.NewGuid();
            int quantity = 5;
            decimal unitPrice = 10m;

            // Act
            var item = new SaleItem(quantity, unitPrice, productId, saleId);

            // Assert
            Assert.Equal(quantity, item.Quantity);
            Assert.Equal(unitPrice, item.UnitPrice);
            Assert.NotEqual(0, item.Discount);
            Assert.Equal(item.Quantity*item.UnitPrice*(1-item.Discount), item.TotalPrice); // 2 * 10
        }

        [Fact]
        public void Should_Throw_Exception_When_Quantity_Exceeds_20()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var saleId = Guid.NewGuid();
            int quantity = 21;
            decimal unitPrice = 10m;

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                new SaleItem(quantity, unitPrice, productId, saleId)
            );

            Assert.Contains("exceeds the 20 items limit", exception.Message);
        }

        [Fact]
        public void Should_Update_Quantity_And_Recalculate_Discount()
        {
            // Arrange
            var item = new SaleItem(2, 10, Guid.NewGuid(), Guid.NewGuid());

            // Act
            item.SetQuantity(5);

            // Assert
            Assert.Equal(0.10m, item.Discount);
            Assert.Equal(5 * 10 * 0.90m, item.TotalPrice);
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

        [Fact]
        public void Should_Update_UnitPrice_And_Recalculate_Total()
        {
            // Arrange
            var item = new SaleItem(5, 10, Guid.NewGuid(), Guid.NewGuid());

            // Act
            item.SetUnitPrice(20);

            // Assert
            Assert.Equal(5 * 20 * 0.90m, item.TotalPrice); // 10% discount remains
        }

        [Fact]
        public void Should_Update_All_Fields_With_Update_Method()
        {
            // Arrange
            var item = new SaleItem(3, 10, Guid.NewGuid(), Guid.NewGuid());

            // Act
            item.Update(item.Id, 10, 50);

            // Assert
            Assert.Equal(10, item.Quantity);
            Assert.Equal(50, item.UnitPrice);
            Assert.Equal(0.20m, item.Discount);
            Assert.Equal(10 * 50 * 0.80m, item.TotalPrice);
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
    }
}
