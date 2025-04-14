using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
            sale.Update(request.SaleNumber, request.Date, request.CustomerId, request.BranchId);

            UpdateSaleItems(sale, request.Items);
            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return _mapper.Map<UpdateSaleResult>(sale);
        }
        private void UpdateSaleItems(Sale sale, List<UpdateSaleItemCommandDto> updatedItems)
        {
            var updatedProductIds = updatedItems.Select(i => i.ProductId).ToHashSet();

            var itemsToRemove = sale.Items.Where(i => !updatedProductIds.Contains(i.ProductId)).ToList();
            foreach (var item in itemsToRemove)
            {
                sale.RemoveItem(item.ProductId); 
            }

            foreach (var updatedItem in updatedItems)
            {
                var existingItem = sale.Items.FirstOrDefault(i => i.ProductId == updatedItem.ProductId);
                if (existingItem != null)
                {
                    existingItem.Update(
                        updatedItem.ProductId,
                        updatedItem.Quantity,
                        updatedItem.UnitPrice
                    );
                }
                else 
                {
                    sale.AddItem(updatedItem.ProductId,
                        updatedItem.Quantity,
                        updatedItem.UnitPrice);
                }
            }

            sale.CalculateTotal();
        }
    }
}
