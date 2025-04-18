using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application.UseCases
{

    public class GetSaleControllerTests
    {
        private readonly TestFixture _fixture;

        public GetSaleControllerTests()
        {
            _fixture = new TestFixture();   
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
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
    }
}
