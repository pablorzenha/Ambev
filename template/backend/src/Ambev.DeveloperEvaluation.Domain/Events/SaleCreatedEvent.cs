﻿namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent
    {
        public Guid SaleId { get; }
        public DateTime CreatedAt { get; }

        public SaleCreatedEvent(Guid saleId)
        {
            SaleId = saleId;
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"SaleCreated | SaleId: {SaleId} | CreatedAt: {CreatedAt}";
        }
    }
}
