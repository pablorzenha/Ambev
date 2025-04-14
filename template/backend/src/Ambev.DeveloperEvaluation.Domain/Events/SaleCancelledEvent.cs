using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelledEvent
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }
        public DateTime CancelledAt { get; }

        public SaleCancelledEvent(Guid saleId, string saleNumber)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
            CancelledAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"SaleCancelled | SaleId: {SaleId} | SaleNumber: {SaleNumber} | CancelledAt: {CancelledAt}";
        }
    }
}
