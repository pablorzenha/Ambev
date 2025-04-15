using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        private readonly Faker _faker;

        public SaleTests()
        {
            _faker = new Faker();
        }

        private Sale CreateSale()
        {
            return new Sale(
                saleNumber: _faker.Random.AlphaNumeric(8),
                customerId: Guid.NewGuid(),
                branchId: Guid.NewGuid(),
                date: _faker.Date.Past(),
                status: SaleStatus.NotCancelled
            );
        }

        [Fact]
        public void Constructor_Should_Initialize_Properties_Correctly()
        {
            var saleNumber = _faker.Random.AlphaNumeric(10);
            var customerId = Guid.NewGuid();
            var branchId = Guid.NewGuid();
            var date = _faker.Date.Past();
            var status = SaleStatus.NotCancelled;

            var sale = new Sale(saleNumber, customerId, branchId, date, status);

            Assert.NotEqual(Guid.Empty, sale.Id);
            Assert.Equal(saleNumber, sale.SaleNumber);
            Assert.Equal(customerId, sale.CustomerId);
            Assert.Equal(branchId, sale.BranchId);
            Assert.Equal(date, sale.Date);
            Assert.Equal(status, sale.Status);
            Assert.True((DateTime.UtcNow - sale.CreatedAt).TotalSeconds < 2);
            Assert.NotNull(sale.Items);
            Assert.Empty(sale.Items);
        }
        [Fact]
        public void Set_SaleNumber_Should_Update_When_Different()
        {
            var sale = CreateSale();
            var newSaleNumber = _faker.Random.AlphaNumeric(10);

            var initialTimestamp = sale.UpdatedAt;
            sale.SetSaleNumber(newSaleNumber);

            Assert.Equal(newSaleNumber, sale.SaleNumber);
            Assert.False(sale.UpdatedAt == initialTimestamp);
        }

        [Fact]
        public void Set_SaleNumber_Should_Not_Update_When_Same()
        {
            var sale = CreateSale();
            var originalUpdatedAt = sale.UpdatedAt;

            sale.SetSaleNumber(sale.SaleNumber);

            Assert.Equal(originalUpdatedAt, sale.UpdatedAt);
        }

        [Fact]
        public void Set_Status_Should_Update_When_Different()
        {
            var sale = CreateSale();
            var initialTimestamp = sale.UpdatedAt;

            sale.SetStatus(SaleStatus.Cancelled);


            Assert.Equal(SaleStatus.Cancelled, sale.Status);
            Assert.True(sale.UpdatedAt != initialTimestamp);
        }

        [Fact]
        public void Set_Status_Should_Not_Update_When_Same()
        {
            var sale = CreateSale();
            var originalUpdatedAt = sale.UpdatedAt;

            sale.SetStatus(sale.Status);

            Assert.Equal(originalUpdatedAt, sale.UpdatedAt);
        }

        [Fact]
        public void Set_Date_Should_Update_When_Different()
        {
            var sale = CreateSale();
            var newDate = DateTime.UtcNow;
            var initialTimestamp = sale.UpdatedAt;
            sale.SetDate(newDate);

            Assert.Equal(newDate, sale.Date);
            Assert.True(sale.UpdatedAt != initialTimestamp);
        }

        [Fact]
        public void Set_Date_Should_Not_Update_When_Same()
        {
            var sale = CreateSale();
            var originalUpdatedAt = sale.UpdatedAt;

            sale.SetDate(sale.Date);

            Assert.Equal(originalUpdatedAt, sale.UpdatedAt);
        }

        [Fact]
        public void Set_Customer_Should_Update_When_Different()
        {
            var sale = CreateSale();
            var newCustomerId = Guid.NewGuid();
            var initialTimestamp = sale.UpdatedAt;
            sale.SetCustomer(newCustomerId);

            Assert.Equal(newCustomerId, sale.CustomerId);
            Assert.True(sale.UpdatedAt != initialTimestamp);
        }

        [Fact]
        public void Set_Customer_Should_Not_Update_When_Same()
        {
            var sale = CreateSale();
            var originalUpdatedAt = sale.UpdatedAt;

            sale.SetCustomer(sale.CustomerId);

            Assert.Equal(originalUpdatedAt, sale.UpdatedAt);
        }

        [Fact]
        public void Set_Branch_Should_Update_When_Different()
        {
            var sale = CreateSale();
            var newBranchId = Guid.NewGuid();
            var initialTimestamp = sale.UpdatedAt;
            sale.SetBranch(newBranchId);

            Assert.Equal(newBranchId, sale.BranchId);
            Assert.True(sale.UpdatedAt != initialTimestamp);
        }

        [Fact]
        public void Set_Branch_Should_Not_Update_When_Same()
        {
            var sale = CreateSale();
            var originalUpdatedAt = sale.UpdatedAt;

            sale.SetBranch(sale.BranchId);

            Assert.Equal(originalUpdatedAt, sale.UpdatedAt);
        }

        [Fact]
        public void Add_Item_Should_Add_SaleItem_And_Return_It()
        {
            var sale = new Sale();
            var productId = Guid.NewGuid();
            int quantity = 5;
            decimal price = 100m;

            var item = sale.AddItem(productId, quantity, price);

            Assert.Single(sale.Items);
            Assert.Equal(productId, item.ProductId);
        }

        [Fact]
        public void Calculate_Total_Should_Sum_All_Items()
        {
            var sale = new Sale();
            sale.AddItem(Guid.NewGuid(), 2, 100); 
            sale.AddItem(Guid.NewGuid(), 4, 50); 

            sale.CalculateTotal();

            Assert.Equal(200 + 180, sale.TotalAmount);
        }

        [Fact]
        public void Replace_Items_Should_Group_Same_Product_And_Throw_If_Unit_Price_Differs()
        {
            var sale = new Sale();
            var productId = Guid.NewGuid();

            sale.AddItem(productId, 5, 10);
            sale.AddItem(productId, 5, 10);

            sale.ReplaceItems();

            Assert.Single(sale.Items);
            Assert.Equal(10, sale.Items[0].Quantity);
        }

        [Fact]
        public void Replace_Items_Should_Throw_When_Total_Quantity_Exceeds_20()
        {
            var sale = new Sale();
            var productId = Guid.NewGuid();

            sale.AddItem(productId, 15, 5);
            sale.AddItem(productId, 10, 5); // total = 25

            var ex = Assert.Throws<InvalidOperationException>(() => sale.ReplaceItems());

            Assert.Equal("Cannot sell more than 20 units of the same product.", ex.Message);
        }

        [Fact]
        public void Set_Status_Should_Update_Status_And_Timestamp()
        {
            var sale = new Sale();
            var initialTimestamp = sale.UpdatedAt;


            sale.SetStatus(SaleStatus.Cancelled);

            Assert.Equal(SaleStatus.Cancelled, sale.Status);
            Assert.True(sale.UpdatedAt != initialTimestamp);
        }
    }
}
