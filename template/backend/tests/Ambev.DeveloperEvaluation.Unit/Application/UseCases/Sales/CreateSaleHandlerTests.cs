using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales
{
    public class CreateSaleHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ISaleService _saleService;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _saleService = Substitute.For<ISaleService>();
            _logger = Substitute.For<ILogger<CreateSaleHandler>>();
            _handler = new CreateSaleHandler(_mapper, _saleService, _logger);
        }

        [Fact(DisplayName = "Given valid sale command When handling Then returns mapped result")]
        public async Task Handle_ValidCommand_ReturnsMappedResult()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = command.SaleNumber,
                Date = command.Date,
                Status = command.Status,
                CustomerId = command.CustomerId,
                BranchId = command.BranchId,
                Items = command.Items.Select(i => new SaleItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                TotalAmount = command.Items.Sum(i => i.Quantity * i.UnitPrice)
            };

            var expectedResult = new CreateSaleResult
            {
                Id = sale.Id,
                TotalAmount = sale.TotalAmount
            };

            _saleService.CreateAsync(command, Arg.Any<CancellationToken>()).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(sale.Id);
            result.TotalAmount.Should().Be(sale.TotalAmount);
        }
    }
}
