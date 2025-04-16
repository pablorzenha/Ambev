using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleItemTests
    {
        private readonly Faker _faker = new();

        private SaleItem CreateSaleItem()
        {
            return new SaleItem(
                quantity: 1,
                unitPrice: 5,
                productId: Guid.NewGuid(),
                saleId: Guid.NewGuid()
            ); ;
        }

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
        }

        [Fact]
        public void Constructor_Should_Throw_When_Quantity_More_Than_20()
        {
            var quantity = 21;
            var unitPrice = 10.0m;
            var productId = Guid.NewGuid();
            var saleId = Guid.NewGuid();

            var item = Assert.Throws<InvalidOperationException>(() => new SaleItem(quantity, unitPrice, productId, saleId));
        }
        [Fact]
        public void Constructor_Should_Set_Discount()
        {
            var unitPrice = 10.0m;
            var productId = Guid.NewGuid();
            var saleId = Guid.NewGuid();
            var quantity1 = 1;
            var quantity2 = 4;
            var quantity3 = 10;

            var item1 = new SaleItem(quantity1, unitPrice, productId, saleId);
            var item2 = new SaleItem(quantity2, unitPrice, productId, saleId);
            var item3 = new SaleItem(quantity3, unitPrice, productId, saleId);

            Assert.Equal(quantity1, item1.Quantity);
            Assert.Equal(0.0m,item1.Discount);
            Assert.Equal(quantity2, item2.Quantity);
            Assert.Equal(0.1m, item2.Discount);
            Assert.Equal(quantity3, item3.Quantity);
            Assert.Equal(0.2m, item3.Discount);
        }

        [Fact]
        public void Set_Unit_Price_Should_Update_When_Different()
        {
            var saleItem = CreateSaleItem();
            var initialUnitPrice =  saleItem.UnitPrice;
            var initialUnitUpdateAt =  saleItem.UpdatedAt;
            saleItem.SetUnitPrice(6);

            Assert.NotEqual(initialUnitPrice, saleItem.UnitPrice);
            Assert.NotEqual(initialUnitUpdateAt, saleItem.UpdatedAt);
        }

        [Fact]
        public void Set_Unit_Price_Should_Not_Update_When_Same()
        {
            var saleItem = CreateSaleItem();
            var initialUnitPrice =  saleItem.UnitPrice;
            var initialUnitUpdateAt =  saleItem.UpdatedAt;
            saleItem.SetUnitPrice(5);

            Assert.Equal(initialUnitPrice, saleItem.UnitPrice);
            Assert.Equal(initialUnitUpdateAt, saleItem.UpdatedAt);
        }

        [Fact]
        public void Set_Quantity_Should_Update_When_Different()
        {
            var saleItem = CreateSaleItem();
            var initialQuantity =  saleItem.Quantity;
            var initialUnitUpdateAt =  saleItem.UpdatedAt;
            saleItem.SetQuantity(2);

            Assert.NotEqual(initialQuantity, saleItem.Quantity);
            Assert.NotEqual(initialUnitUpdateAt, saleItem.UpdatedAt);
        }

        [Fact]
        public void Set_Quantity_Should_Not_Update_When_Same()
        {
            var saleItem = CreateSaleItem();
            var initialQuantity =  saleItem.Quantity;
            var initialUnitUpdateAt =  saleItem.UpdatedAt;
            saleItem.SetQuantity(1);

            Assert.Equal(initialQuantity, saleItem.Quantity);
            Assert.Equal(initialUnitUpdateAt, saleItem.UpdatedAt);
        }




    }
}
