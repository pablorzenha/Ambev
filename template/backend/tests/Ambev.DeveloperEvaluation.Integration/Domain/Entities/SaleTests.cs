using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Domain.Entities
{
    public class SaleTests
    {
        private readonly Guid _customerId = Guid.NewGuid();
        private readonly Guid _branchId = Guid.NewGuid();

        [Fact]
        public void Should_Create_Sale_With_Items_And_Calculate_Total()
        {
            // Arrange
            var sale = new Sale("VENDA001", _customerId, _branchId, DateTime.Today, SaleStatus.NotCancelled);

            // Act
            sale.AddItem(Guid.NewGuid(), quantity: 2, unitPrice: 10m);
            sale.AddItem(Guid.NewGuid(), quantity: 1, unitPrice: 20m);

            // Assert
            Assert.Equal(40m, sale.TotalAmount);
            Assert.Equal(2, sale.Items.Count);
        }

        [Fact]
        public void Should_Update_Item_And_Recalculate_Total()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var sale = new Sale("VENDA002", _customerId, _branchId, DateTime.Today, SaleStatus.NotCancelled);
            sale.AddItem(productId, 1, 10m);

            // Act
            sale.UpdateItem(productId, 3, 15m);

            // Assert
            Assert.Equal(45m, sale.TotalAmount);
        }

        [Fact]
        public void Should_Consolidate_Duplicate_Items_Successfully()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var sale = new Sale("VENDA003", _customerId, _branchId, DateTime.Today, SaleStatus.NotCancelled);

            sale.AddItem(productId, 2, 10m);
            sale.AddItem(productId, 3, 10m);

            // Act
            sale.ReplaceItems();

            // Assert
            Assert.Single(sale.Items);
            Assert.Equal(5, sale.Items.First().Quantity);
            Assert.Equal(45m, sale.TotalAmount);
        }

        [Fact]
        public void Should_Not_Consolidate_If_Same_Products_Have_Different_Prices()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var sale = new Sale("VENDA004", _customerId, _branchId, DateTime.Today, SaleStatus.NotCancelled);

            sale.AddItem(productId, 2, 10m);
            sale.AddItem(productId, 3, 15m);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => sale.ReplaceItems());
            Assert.Equal("Multiple unit prices found for the same product.", ex.Message);
        }

        [Fact]
        public void Should_Not_Allow_More_Than_20_Units_Of_The_Same_Product()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var sale = new Sale("VENDA005", _customerId, _branchId, DateTime.Today, SaleStatus.NotCancelled);

            sale.AddItem(productId, 10, 5m);
            sale.AddItem(productId, 11, 5m); // total = 21

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => sale.ReplaceItems());
            Assert.Equal("Cannot sell more than 20 units of the same product.", ex.Message);
        }
    }
}
