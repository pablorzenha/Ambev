using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents the result of a sale creation operation.
    /// </summary>
    public class CreateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the number of the sale.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of the sale.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets customer of the sale.
        /// </summary>
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the branch of the sale.
        /// </summary>
        public string BranchId { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the Status of the sale.
        /// </summary>
        public SaleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the items of the sale.
        /// </summary>
        public List<CreateSaleItemResult> Items { get; set; } = new();
    }
    public class CreateSaleItemResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the item sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the sale' product.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the item sale.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the item sale.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount of the item sale.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total price of the item sale.
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
