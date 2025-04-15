using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a request to create a new sale in the system.
    /// </summary>
    public class CreateSaleRequest
    {
        /// <summary>
        /// Gets or sets the SaleNumber.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the external customer identifier.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the external branch identifier.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the status assigned to the sale.
        /// </summary>
        public SaleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public List<CreateSaleItemRequest> Items { get; set; } = new();
    }
    public class CreateSaleItemRequest
    {
        /// <summary>
        /// Gets or sets the external product identifier.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

    }
}
