namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleUpdatedEvent
    {
        public Guid SaleId { get; }
        public DateTime UpdatedAt { get; }

        public SaleUpdatedEvent(Guid saleId)
        {
            SaleId = saleId;
            UpdatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"SaleUpdated | SaleId: {SaleId} | UpdatedAt: {UpdatedAt}";
        }
    }
}
