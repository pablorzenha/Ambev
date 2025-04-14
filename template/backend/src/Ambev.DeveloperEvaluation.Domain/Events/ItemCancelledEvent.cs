using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class ItemCancelledEvent
    {
        public Guid SaleId { get; }
        public Guid ProductId { get; }
        public string SaleNumber { get; }
        public DateTime CancelledAt { get; }

        public ItemCancelledEvent(Guid saleId, Guid productId, string saleNumber)
        {
            SaleId = saleId;
            ProductId = productId;
            SaleNumber = saleNumber;
            CancelledAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"ItemCancelled | SaleId: {SaleId} | ProductId: {ProductId} | SaleNumber: {SaleNumber} | CancelledAt: {CancelledAt}";
        }
    }
}
