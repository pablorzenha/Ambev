using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
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
        public DateTime Date { get; set; }

        public string CustomerId { get; set; } = string.Empty;
        public string BranchId { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }
        public SaleStatus Status { get; set; }
        public List<CreateSaleItemResultDto> Items { get; set; } = new();



    }
}
