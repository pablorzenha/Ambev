using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application
{

    public class GetSaleControllerTests
    {
        private readonly TestFixture _fixture;

        public GetSaleControllerTests()
        {
            _fixture = new TestFixture();
        }

        private async Task<Guid> CreateSaleForTestingAsync()
        {
            var request = new CreateSaleRequest
            {
                SaleNumber = Guid.NewGuid().ToString(),
                Date = DateTime.UtcNow.AddDays(-1),
                CustomerId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                Status = SaleStatus.NotCancelled,
                Items = new List<CreateSaleItemRequest>
            {
                new CreateSaleItemRequest
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 3,
                    UnitPrice = 50.00m
                }
            }
            };

            var response = await _fixture.Client.PostAsJsonAsync("/api/sales", request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
            return content.Data.Id;
        }

        [Fact]
        public async Task CreateSale_Should_ReturnOK_WhenValidRequest()
        {
            var request = new CreateSaleRequest
            {
                SaleNumber = Guid.NewGuid().ToString().Substring(0, 12),
                Date = DateTime.UtcNow.AddDays(-1),
                CustomerId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                Status = SaleStatus.NotCancelled,
                Items = new List<CreateSaleItemRequest>
                {
                    new CreateSaleItemRequest
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 5,
                        UnitPrice = 10.50m
                    }
                }
            };
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _fixture.Client.PostAsync("/api/Sales", content);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetSaleById_Should_Return_Valid_Sale()
        {
            // Arrange
            var id = await CreateSaleForTestingAsync();

            // Act
            var response = await _fixture.Client.GetAsync($"/api/sales/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetSaleResponse>>();
            Assert.Equal(id, content.Data.Id);
        }

        [Fact]
        public async Task GetSaleList_Should_ReturnCreated_WhenValidRequest()
        {
            var response = await _fixture.Client.GetAsync("/api/Sales");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteSale_Should_Return_NoContent()
        {
            // Arrange
            var id = await CreateSaleForTestingAsync();

            // Act
            var response = await _fixture.Client.DeleteAsync($"/api/Sales/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            Func<Task> act = async () => {
                var response = await _fixture.Client.GetAsync($"/api/Sales/{id}");
                response.EnsureSuccessStatusCode(); 
            };
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task UpdateSale_Should_Return_Updated_Sale()
        {
            // Arrange
            var id = await CreateSaleForTestingAsync();

            var updateRequest = new UpdateSaleRequest
            {
                Id = id,
                SaleNumber = "UPDATED-123",
                Date = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                Status = SaleStatus.Cancelled,
                Items = new List<UpdateSaleItemRequest>
            {
                new UpdateSaleItemRequest
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 2,
                    UnitPrice = 20.0m
                }
            }
            };

            // Act
            var response = await _fixture.Client.PutAsJsonAsync($"/api/sales/{id}", updateRequest);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateSaleResponse>>();
            Assert.Equal(updateRequest.Id, content.Data.Id);
            Assert.Equal(updateRequest.SaleNumber, content.Data.SaleNumber);
            Assert.Equal(updateRequest.CustomerId, content.Data.CustomerId);
            Assert.Equal(updateRequest.BranchId, content.Data.BranchId);
        }

    }
}
