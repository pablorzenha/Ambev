using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales
{
    public class GetSaleHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ISaleService _saleService;
        private readonly GetSaleHandler _handler;

        public GetSaleHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _saleService = Substitute.For<ISaleService>();
            _handler = new GetSaleHandler(_mapper, _saleService);
        }

        [Fact]
        public async Task Handle_Should_Return_Mapped_Result_When_Sale_IsFound()
        {
            // Arrange
            var command = GetSaleHandlerTestData.GenerateValidCommand();
            var fakeSale = new Sale
            {
                Id = command.Id,
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
            };

            var expectedResult = new GetSaleResult
            {
                Id = fakeSale.Id,
                TotalAmount = fakeSale.TotalAmount,
                Date = fakeSale.Date,
                Items = fakeSale.Items.Select(i => new GetSaleItemResult
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            _saleService.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                        .Returns(fakeSale);

            _mapper.Map<GetSaleResult>(fakeSale).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            result.TotalAmount.Should().Be(fakeSale.TotalAmount);

            await _saleService.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            _mapper.Received(1).Map<GetSaleResult>(fakeSale);
        }

     
    }
}
