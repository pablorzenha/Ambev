using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        /// <summary>
        /// Gets or sets unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

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

        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the list of items in the sale.
        /// </summary>
        public SaleStatus Status { get; set; }

        public List<CreateSaleItemReponseDto> Items { get; set; } = new();
    }

}
