using Ambev.DeveloperEvaluation.Application.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SaleItemService : ISaleItemService
    {

        private readonly ILogger<SaleItemService> _logger;
        public SaleItemService(ILogger<SaleItemService> logger)
        {
            _logger = logger;
        }

        public void Update(Sale sale, List<UpdateSaleItemCommand> updatedItems)
        {
            CancelledSaleItems(sale, updatedItems);
            var verifiedList = VerifySaleItems(sale, updatedItems);
            UpdateSaleItems(sale, verifiedList);

        }
        private void CancelledSaleItems(Sale sale, List<UpdateSaleItemCommand> updatedItems)
        {
            var updatedProductIds = updatedItems.Select(i => i.ProductId).ToHashSet();
            var itemsToRemove = sale.Items.Where(i => !updatedProductIds.Contains(i.ProductId)).ToList();

            foreach (var item in itemsToRemove)
            {
                sale.RemoveItem(item.ProductId);
                _logger.LogInformation("Publishing event: {@DomainEvent}", new ItemCancelledEvent(sale.Id, item.ProductId, sale.SaleNumber));
            }
        }
        private Dictionary<Guid, ISaleItem> VerifySaleItems(Sale sale, List<UpdateSaleItemCommand> updatedItems)
        {
            var verfiedList = new Dictionary<Guid, ISaleItem>();
            foreach (var updatedItem in updatedItems)
            {
                var existingItem = sale.Items.FirstOrDefault(i => i.ProductId == updatedItem.ProductId);
                if (verfiedList.TryGetValue(updatedItem.ProductId, out var groupedItem))
                {
                    groupedItem.Quantity += updatedItem.Quantity;

                    if (groupedItem.UnitPrice != updatedItem.UnitPrice)
                        throw new InvalidOperationException("Multiple unit prices found for the same product.");
                }
                else
                {
                    if (existingItem != null)
                        verfiedList[updatedItem.ProductId] = new SaleItem(existingItem.Quantity + updatedItem.Quantity, existingItem.UnitPrice, existingItem.ProductId, sale.Id);
                    else
                        verfiedList[updatedItem.ProductId] = new SaleItem(updatedItem.Quantity, updatedItem.UnitPrice, updatedItem.ProductId, sale.Id);
                }
            }

            return verfiedList;

        }
        private void UpdateSaleItems(Sale sale, Dictionary<Guid, ISaleItem> verifiedList)
        {
            foreach (var groupedItem in verifiedList.Values)
            {
                var existingItem = sale.Items.FirstOrDefault(i => i.ProductId == groupedItem.ProductId);

                if (existingItem != null)
                {
                    sale.UpdateItem(groupedItem.ProductId, groupedItem.Quantity, groupedItem.UnitPrice);
                }
                else
                {
                    sale.AddItem(groupedItem.ProductId, groupedItem.Quantity, groupedItem.UnitPrice);
                }
            }

            sale.CalculateTotal();
        }
    }
}
