using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales.TestData;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales
{
    public class UpdateSaleHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ISaleService _saleService;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _saleService = Substitute.For<ISaleService>();
            _logger = Substitute.For<ILogger<CreateSaleHandler>>();
            _handler = new UpdateSaleHandler(_saleService, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsMappedResult()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();
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

            var expectedResult = new UpdateSaleResult
            {
                Id = sale.Id,
                SaleNumber = command.SaleNumber,
                Date = new Faker().Date.Past(),
                Status = command.Status,
                CustomerId = command.CustomerId.ToString(),
                BranchId = command.BranchId.ToString(),
                Items = command.Items.Select(i => new UpdateSaleItemResult
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                TotalAmount = command.Items.Sum(i => i.Quantity * i.UnitPrice)
            };

            _saleService.UpdateAsync(command, Arg.Any<CancellationToken>()).Returns(sale);
            _mapper.Map<UpdateSaleResult>(sale).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(sale.Id);
            result.Date.Should().NotBe(sale.Date);
        }
    }
}
