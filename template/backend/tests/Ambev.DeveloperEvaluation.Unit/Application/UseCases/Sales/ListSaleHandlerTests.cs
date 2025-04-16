using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.ListSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales
{
    public class ListSaleHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ISaleService _saleService;
        private readonly ILogger<ListSaleHandler> _logger;
        private readonly ListSaleHandler _handler;

        public ListSaleHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _saleService = Substitute.For<ISaleService>();
            _logger = Substitute.For<ILogger<ListSaleHandler>>();
            _handler = new ListSaleHandler(_saleService, _mapper );
        }

        [Fact]
        public async Task Handle_Should_Return_Mapped_ListResult()
        {
            // Arrange
            var command = ListSaleHandlerTestData.GenerateValidCommand();
            var fakeSale = new List<Sale>()
            {
                new Sale
                {
                    Id = Guid.NewGuid(),
                    TotalAmount = 999.99m,
                    Date = DateTime.UtcNow,
                    CustomerId = Guid.NewGuid(),
                    Items = new List<SaleItem>
                    {
                        new SaleItem
                        {
                            ProductId = Guid.NewGuid(),
                            Quantity = 1,
                            UnitPrice = 999.99m
                        }
                    }
                }
            };

            var expectedResult = new ListSaleResult(fakeSale.Count,fakeSale);
           

            _saleService.GetAllAsync(command, Arg.Any<CancellationToken>())
                        .Returns(fakeSale);
            _saleService.CountAsync(Arg.Any<CancellationToken>()).Returns(fakeSale.Count);

            _mapper.Map<ListSaleResult>(fakeSale).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.TotalSize.Should().Be(1); 
            result.Items.Should().BeEquivalentTo(fakeSale); 

            await _saleService.Received(1).GetAllAsync(command, Arg.Any<CancellationToken>()); 
        }
    }
}
