namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }
        public DateTime CreatedAt { get; }

        public SaleCreatedEvent(Guid saleId, string saleNumber)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"SaleCreated | SaleId: {SaleId} | SaleNumber: {SaleNumber} | CreatedAt: {CreatedAt}";
        }
    }
}
