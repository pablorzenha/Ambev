using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.ListSale
{
    public class ListSaleCommand : IRequest<ListSaleResult>
    {
        /// <summary>
        /// Gets or sets page of the list sale.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets take elements of the list sale.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets order of the list sale.
        /// </summary>
        public string? Order { get; set; }

        /// <summary>
        /// Gets or sets total size of the list sale.
        /// </summary>
        public int TotalSize { get; set; }
    }
}
