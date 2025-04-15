using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        /// <summary>
        /// Gets or sets the sale ID.
        /// </summary>
        public Guid Id { get; set; } 

        /// <summary>
        /// Gets or sets the sale date.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sale date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the external customer identifier.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the external branch identifier where the sale was made.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the status sale.
        /// </summary>
        /// 
        public SaleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the list of items in the sale.
        /// </summary>
        public List<UpdateSaleItemRequest> Items { get; set; } = new();

    }
    public class UpdateSaleItemRequest
    {
        /// <summary>
        /// Gets or sets the external product identifier.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
