using Ambev.DeveloperEvaluation.Application.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Features.Sales.ListSale;
using Ambev.DeveloperEvaluation.Application.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleValidation _saleValidation;
        private readonly ISaleItemService _saleSaleItemService;
        private readonly IDomainEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly ILogger<SaleService> _logger;


        public SaleService(ISaleRepository saleRepository, IMapper mapper, ILogger<SaleService> logger,
            ISaleValidation saleValidation, ISaleItemService saleItemService, IDomainEventPublisher eventPublisher)
        {
            _saleRepository = saleRepository;
            _saleSaleItemService = saleItemService;
            _mapper = mapper;
            _logger = logger;
            _saleValidation = saleValidation;
            _eventPublisher = eventPublisher;

        }

        public async Task<Sale> CreateAsync(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            await _saleValidation.SaleNumberIsUniqueAsync(command.SaleNumber, cancellationToken);

            var sale = _mapper.Map<Sale>(command);
            sale.ReplaceItems();
            sale.CalculateTotal();

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            var @event = new SaleCreatedEvent(createdSale.Id);
            _eventPublisher.Publish(@event);

            return createdSale;
        }
        public async Task<Sale> GetByIdAsync(Guid saleId, CancellationToken cancellationToken)
        {
            var sale = await _saleValidation.ExistsSaleIdAsync(saleId, cancellationToken);
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
            var sale = await _saleValidation.ExistsSaleIdAsync(command.Id, cancellationToken);
            await _saleRepository.DeleteAsync(sale, cancellationToken);

            return sale;
        }
        public async Task<Sale> UpdateAsync(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleValidation.ExistsSaleIdAsync(request.Id, cancellationToken);
            if (sale.SaleNumber != request.SaleNumber)
                await _saleValidation.SaleNumberIsUniqueAsync(request.SaleNumber, cancellationToken);

            var currentUpdatedAt = sale.UpdatedAt;
            var currentStatus = sale.Status;

            sale.Update(request.SaleNumber, request.Date, request.CustomerId, request.BranchId, request.Status);

            if (currentUpdatedAt != sale.UpdatedAt)
            {
                var @event = new SaleUpdatedEvent(sale.Id);
                _eventPublisher.Publish(@event);
            }
            if (currentStatus != sale.Status && sale.Status == SaleStatus.Cancelled)
            {
                var cancelledEvent = new SaleCancelledEvent(sale.Id, sale.SaleNumber);
                _logger.LogInformation("Publishing event: {@Event}", cancelledEvent);
            }

            _saleSaleItemService.Update(sale, request.Items);
            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return sale;
        }


    }
}

