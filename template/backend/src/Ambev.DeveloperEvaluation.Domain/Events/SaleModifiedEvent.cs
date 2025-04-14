using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }
        public DateTime ModifiedAt { get; }

        public SaleModifiedEvent(Guid saleId, string saleNumber)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
            ModifiedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"SaleModified | SaleId: {SaleId} | SaleNumber: {SaleNumber} | ModifiedAt: {ModifiedAt}";
        }
    }
}
