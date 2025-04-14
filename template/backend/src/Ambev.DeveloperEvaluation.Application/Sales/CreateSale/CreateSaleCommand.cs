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
        public string SaleNumber { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public Guid CustomerId { get; set; }

        public Guid BranchId { get; set; }

        public List<CreateSaleItemCommandDto> Items { get; set; } = new();
        public SaleStatus Status { get; set; }



    }


}
