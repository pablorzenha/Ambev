using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Sales.UpdateSale
{
    /// <summary>
    /// Command for update a new sale.
    /// </summary>
    /// <remarks>
    /// This command contains the required information to register a sale,
    /// including the date, sale number, customer, branch, status and sale items.
    /// 
    /// It implements <see cref="IRequest{TResponse}"/> to return a <see cref="UpdateSaleCommand"/>.
    /// The command is validated using the <see cref="UpdateSaleValidator"/>.
    /// </remarks>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        /// <summary>
        /// Gets or sets the SaleNumber of the sale to be updated.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Date of the sale to be updated.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the sale number of the sale to be updated.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the customer of the sale to be updated.
        /// </summary>
        public Guid CustomerId { get; set; }


        /// <summary>
        /// Gets or sets the branch of the sale to be updated.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the status of the sale to be updated.
        /// </summary>
        public SaleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the items of the sale to be updated.
        /// </summary>
        public List<UpdateSaleItemCommand> Items { get; set; } = new();
    }
    public class UpdateSaleItemCommand
    {
        /// <summary>
        /// Gets or sets the product of the sale to be updated.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the sale to be updated.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the sale to be updated.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount of the sale to be updated.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total price of the sale to be updated.
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
