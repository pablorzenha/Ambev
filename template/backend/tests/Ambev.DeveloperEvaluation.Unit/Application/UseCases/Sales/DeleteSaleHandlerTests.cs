using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales
{
    public class DeleteSaleHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly ISaleService _saleService;
        private readonly DeleteSaleHandler _handler;

        public DeleteSaleHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _saleService = Substitute.For<ISaleService>();
            _handler = new DeleteSaleHandler(_mapper, _saleService);
        }

        [Fact]
        public async Task Handle_Should_Return_Mapped_Result_When_Sale_Is_Deleted()
        {
            // Arrange
            var command = DeleteSaleHandlerTestData.GenerateValidCommand();
            var fakeSale = new Sale
            {
                Id = command.Id,
                TotalAmount = 150m
            };

            var expectedResult = new DeleteSaleResult();

            _saleService
                .DeleteAsync(command, Arg.Any<CancellationToken>())
                .Returns(fakeSale);

            _mapper
                .Map<DeleteSaleResult>(fakeSale)
                .Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);

        }
    }
}
