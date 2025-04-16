using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    /// <remarks>
    /// This command contains the required information to register a sale,
    /// including the date, sale number, customer, branch, status and sale items.
    /// 
    /// It implements <see cref="IRequest{TResponse}"/> to return a <see cref="CreateSaleResult"/>.
    /// The command is validated using the <see cref="CreateSaleValidator"/>.
    /// </remarks>
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// Gets or sets the SaleNumber of the sale to be created.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Date of the sale to be created.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the CustomerId of the sale to be created.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the BranchId of the sale to be created.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the Status of the sale to be created.
        /// </summary>
        public SaleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the Items of the sale to be created.
        /// </summary>
        public List<CreateSaleItemCommand> Items { get; set; } = new();
    }
    public class CreateSaleItemCommand
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
