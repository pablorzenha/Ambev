using Ambev.DeveloperEvaluation.Domain.Enums;


namespace Ambev.DeveloperEvaluation.Application.Features.Sales.UpdateSale
{
    /// <summary>
    /// Represents the result of a sale updated operation.
    /// </summary>
    public class UpdateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the updated sale.
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
        public List<UpdateSaleItemResult> Items { get; set; } = new();

    }

    public class UpdateSaleItemResult
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        public Guid Id { get; set; }
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

        /// <summary>
        /// Gets or sets the discount of the product.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total Price of the product.
        /// </summary>
        public decimal TotalPrice { get; set; }
    }

}
