using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Command for creating a new sale.
    /// </summary>
    /// <remarks>
    /// This command contains the required information to register a sale,
    /// including the date, customer, branch, and sale items.
    /// 
    /// It implements <see cref="IRequest{TResponse}"/> to return a <see cref="CreateSaleResult"/>.
    /// The command is validated using the <see cref="CreateSaleCommandValidator"/>.
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
        /// Gets or sets the Items of the sale to be created.
        /// </summary>
        public List<CreateSaleItemCommandDto> Items { get; set; } = new();

        /// <summary>
        /// Gets or sets the Status of the sale to be created.
        /// </summary>
        public SaleStatus Status { get; set; }

    }
}
