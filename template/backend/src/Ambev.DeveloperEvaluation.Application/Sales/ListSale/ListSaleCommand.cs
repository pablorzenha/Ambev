using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class ListSaleCommand : IRequest<ListSaleResult>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string? Order { get; set; }
        public int TotalSize { get; set; } 
    }
}
