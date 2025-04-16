using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
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

        [Fact]
        public void Constructor_Should_Initialize_Properties_Correctly()
        {
            var saleNumber = SaleTestData.GenerateValidSaleNumber();
            var customerId = SaleTestData.GenerateCustomerId();
            var branchId = SaleTestData.GenerateBranchId();
            var date = SaleTestData.GenerateValidPastDate();
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
            var sale = SaleTestData.GenerateValidSale();
            var newSaleNumber = SaleTestData.GenerateValidSaleNumber();

            var initialTimestamp = sale.UpdatedAt;
            sale.SetSaleNumber(newSaleNumber);

            Assert.Equal(newSaleNumber, sale.SaleNumber);
            Assert.NotEqual(sale.UpdatedAt,initialTimestamp);
        }

        [Fact]
        public void Set_SaleNumber_Should_Not_Update_When_Same()
        {
            var sale = SaleTestData.GenerateValidSale();
            var originalUpdatedAt = sale.UpdatedAt;

            sale.SetSaleNumber(sale.SaleNumber);

            Assert.Equal(originalUpdatedAt, sale.UpdatedAt);
        }

        [Fact]
        public void Set_Status_Should_Update_When_Different()
        {
            var sale = SaleTestData.GenerateValidSale();
            var initialTimestamp = sale.UpdatedAt;

            sale.SetStatus(SaleStatus.Cancelled);


            Assert.Equal(SaleStatus.Cancelled, sale.Status);
            Assert.NotEqual(sale.UpdatedAt, initialTimestamp);
        }

        [Fact]
        public void Set_Status_Should_Not_Update_When_Same()
        {
            var sale = SaleTestData.GenerateValidSale();
            var originalUpdatedAt = sale.UpdatedAt;

            sale.SetStatus(sale.Status);

            Assert.Equal(originalUpdatedAt, sale.UpdatedAt);
        }

        [Fact]
        public void Set_Date_Should_Update_When_Different()
        {
            var sale = SaleTestData.GenerateValidSale();
            var newDate = DateTime.UtcNow;
            var initialTimestamp = sale.UpdatedAt;
            sale.SetDate(newDate);

            Assert.Equal(newDate, sale.Date);
            Assert.NotEqual(sale.UpdatedAt, initialTimestamp);
        }

        [Fact]
        public void Set_Date_Should_Not_Update_When_Same()
        {
            var sale = SaleTestData.GenerateValidSale();
            var originalUpdatedAt = sale.UpdatedAt;

            sale.SetDate(sale.Date);

            Assert.Equal(originalUpdatedAt, sale.UpdatedAt);
        }

        [Fact]
        public void Set_Customer_Should_Update_When_Different()
        {
            var sale = SaleTestData.GenerateValidSale();
            var newCustomerId = Guid.NewGuid();
            var initialTimestamp = sale.UpdatedAt;
            sale.SetCustomer(newCustomerId);

            Assert.Equal(newCustomerId, sale.CustomerId);
            Assert.NotEqual(sale.UpdatedAt, initialTimestamp);
        }

        [Fact]
        public void Set_Customer_Should_Not_Update_When_Same()
        {
            var sale = SaleTestData.GenerateValidSale();
            var originalUpdatedAt = sale.UpdatedAt;

            sale.SetCustomer(sale.CustomerId);

            Assert.Equal(originalUpdatedAt, sale.UpdatedAt);
        }

        [Fact]
        public void Set_Branch_Should_Update_When_Different()
        {
            var sale = SaleTestData.GenerateValidSale();
            var newBranchId = Guid.NewGuid();
            var initialTimestamp = sale.UpdatedAt;
            sale.SetBranch(newBranchId);

            Assert.Equal(newBranchId, sale.BranchId);
            Assert.NotEqual(sale.UpdatedAt, initialTimestamp);
        }

        [Fact]
        public void Set_Branch_Should_Not_Update_When_Same()
        {
            var sale = SaleTestData.GenerateValidSale();
            var originalUpdatedAt = sale.UpdatedAt;

            sale.SetBranch(sale.BranchId);

            Assert.Equal(originalUpdatedAt, sale.UpdatedAt);
        }

        [Fact]
        public void Add_Sale_Item_In_Sale()
        {
            var sale = SaleTestData.GenerateValidSale();
            var productId = Guid.NewGuid();
            int quantity = 5;
            decimal price = 100m;
            var initialListCount = sale.Items.Count;

            var item = sale.AddItem(productId, quantity, price);

            Assert.Single(sale.Items);
            Assert.NotEqual(initialListCount,sale.Items.Count);
            Assert.Equal(productId, item.ProductId);
        }

        [Fact]
        public void Update_Sale_Item_In_Sale()
        {
            var sale = SaleTestData.GenerateValidSale();
            var productId = Guid.NewGuid();
            int quantity = 5;
            decimal price = 100m;

            var item = sale.AddItem(productId, quantity, price);
            sale.UpdateItem(productId, 1, 20);

            Assert.Single(sale.Items);
            Assert.NotEqual(quantity, item.Quantity);
        }

        [Fact]
        public void Remove_Sale_Item_In_Sale()
        {
            var sale = SaleTestData.GenerateValidSale();
            var productId = Guid.NewGuid();
            int quantity = 5;
            decimal price = 100m;

            sale.AddItem(productId, quantity, price);
            sale.RemoveItem(productId);

            Assert.Empty(sale.Items);
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
