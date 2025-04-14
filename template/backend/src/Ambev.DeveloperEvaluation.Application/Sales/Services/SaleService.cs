
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Ambev.DeveloperEvaluation.Application.Sales.Services.Interfaces;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.Dtos;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SaleService> _logger;

        public SaleService(ISaleRepository saleRepository, IMapper mapper, ILogger<SaleService> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Sale> CreateAsync(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var existingSale = await _saleRepository.GetBySaleNameAsync(command.SaleNumber, cancellationToken);
            if (existingSale != null)
                throw new InvalidOperationException($"Sale with sale number '{command.SaleNumber}' already exists");

            var sale = _mapper.Map<Sale>(command);
            sale.ReplaceItems();
            sale.CalculateTotal();
            

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            var @event = new SaleCreatedEvent(createdSale.Id, createdSale.SaleNumber);
            _logger.LogInformation("Publishing event: {@Event}", @event);

            return createdSale;
        }
        public async Task<Sale> GetByIdAsync(Guid saleId, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId, cancellationToken)
                        ?? throw new KeyNotFoundException($"Sale with ID {saleId} not found");

            return sale;
        }
        public async Task<ListSaleResult> GetAllAsync(ListSaleCommand request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllPageAsync(request.Skip, request.Take, request.Order, cancellationToken);
            var totalSize = await _saleRepository.CountAsync(cancellationToken);
            return new ListSaleResult(totalSize, sales);
        }
        public async Task<Sale> DeleteAsync(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken)
               ?? throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

            await _saleRepository.DeleteAsync(sale, cancellationToken);

            return sale;
        }
        public async Task<Sale> UpdateAsync(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            var currentUpdatedAt = sale.UpdatedAt;
            var currentStatus = sale.Status;

            sale.Update(request.SaleNumber, request.Date, request.CustomerId, request.BranchId, request.Status);

            UpdateSaleItems(sale, request.Items);

            if (currentUpdatedAt != sale.UpdatedAt)
            {
                var @event = new SaleModifiedEvent(sale.Id, sale.SaleNumber);
                _logger.LogInformation("Publishing event: {@Event}", @event);
            }

            if (currentStatus != sale.Status && sale.Status == SaleStatus.Cancelled)
            {
                var cancelledEvent = new SaleCancelledEvent(sale.Id, sale.SaleNumber);
                _logger.LogInformation("Publishing event: {@Event}", cancelledEvent);
            }

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return sale;
        }
        private void UpdateSaleItems(Sale sale, List<UpdateSaleItemCommandDto> updatedItems)
        {
            var updatedProductIds = updatedItems.Select(i => i.ProductId).ToHashSet();

            var itemsToRemove = sale.Items.Where(i => !updatedProductIds.Contains(i.ProductId)).ToList();
            foreach (var item in itemsToRemove)
            {
                sale.RemoveItem(item.ProductId);

                var cancelledItemEvent = new ItemCancelledEvent(sale.Id, item.ProductId, sale.SaleNumber);
                _logger.LogInformation("Publishing event: {@Event}", cancelledItemEvent);
            }

            var grouped = new Dictionary<Guid, ISaleItem>();
            foreach (var updatedItem in updatedItems)
            {
                var existingItem = sale.Items.FirstOrDefault(i => i.ProductId == updatedItem.ProductId);
                if (existingItem != null)
                {
                    if (!grouped.ContainsKey(existingItem.ProductId))
                    {
                        grouped[existingItem.ProductId] = existingItem;
                        existingItem.Update(
                            updatedItem.ProductId,
                            updatedItem.Quantity,
                            updatedItem.UnitPrice
                        );
                    }
                    else
                    {
                        var existing = grouped[existingItem.ProductId];

                        if (existing.UnitPrice != existingItem.UnitPrice)
                            throw new InvalidOperationException("Multiple unit prices found for the same product.");

                        existing.Quantity += updatedItem.Quantity;
                        existing.Update(
                            existing.ProductId,
                            existing.Quantity,
                            existing.UnitPrice
                        );
                    }
                }
                else
                {
                    var saleItem = sale.AddItem(updatedItem.ProductId,
                        updatedItem.Quantity,
                        updatedItem.UnitPrice);

                    grouped[updatedItem.ProductId] = saleItem;
                }
            }

            sale.CalculateTotal();
        }
    }
}

