using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application.UseCases
{
    public class CreateSaleControllerTests
    {
      
        private readonly TestFixture _fixture;

        public CreateSaleControllerTests()
        {
            _fixture = new TestFixture();   
        }

        [Fact]
        public async Task GetSale_Should_ReturnCreated_WhenValidRequest()
        {
            var response = await _fixture.Client.GetAsync("/api/Sales");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }


    }
}
