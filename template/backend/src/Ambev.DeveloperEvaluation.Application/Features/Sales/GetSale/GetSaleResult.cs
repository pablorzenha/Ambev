using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.GetSale
{
    public class GetSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the sale.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the number of the sale.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the customer of the sale.
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
        /// Gets or sets the status of the sale.
        /// </summary>
        public SaleItemStatus Status { get; set; }


        /// <summary>
        /// Gets or sets the items of the sale.
        /// </summary>
        public List<GetSaleItemResult> Items { get; set; } = new();
    }
    public class GetSaleItemResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the item.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the item.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount of the item.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total price of the item.
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
