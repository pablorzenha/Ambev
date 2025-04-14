using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler for processing CreateSaleCommand requests.
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">Logger for publishing internal events</param>
        public CreateSaleHandler(
            ISaleRepository saleRepository,
            IMapper mapper,
            ILogger<CreateSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the CreateSaleCommand request.
        /// </summary>
        /// <param name="command">The CreateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of sale creation</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetBySaleNameAsync(command.SaleNumber, cancellationToken);
            if (existingSale != null)
                throw new InvalidOperationException($"Sale with sale number '{command.SaleNumber}' already exists");

            var sale = _mapper.Map<Sale>(command);
            sale.ReplaceItems();
            _logger.LogInformation("Event: SaleNumberValidation | SaleId: {SaleNumber} ", sale.SaleNumber);

            sale.TotalAmount = sale.Items.Sum(i => i.TotalPrice );

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            _logger.LogInformation("Event: SaleCreated | SaleId: {SaleId} | Amount: {Amount}", createdSale.Id, createdSale.TotalAmount);
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }

        public static List<CreateSaleItemCommandDto> ConsolidateItems(List<CreateSaleItemCommandDto> items)
        {
            var grouped = new Dictionary<Guid, CreateSaleItemCommandDto>();

            foreach (var item in items)
            {
                if (!grouped.ContainsKey(item.ProductId))
                {
                    grouped[item.ProductId] = new CreateSaleItemCommandDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                }
                else
                {
                    var existing = grouped[item.ProductId];

                    if (existing.UnitPrice != item.UnitPrice)
                        throw new InvalidOperationException("Multiple unit prices found for the same product.");

                    existing.Quantity += item.Quantity;
                }
            }

            foreach (var entry in grouped.Values)
            {
                if (entry.Quantity > 20)
                    throw new InvalidOperationException("Cannot sell more than 20 units of the same product.");
            }

            return grouped.Values.ToList();
        }
    }

}
