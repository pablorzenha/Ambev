using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Sales.ListSale
{
    public class ListSaleResult
    {
        public int TotalSize { get; set; }
        public List<Sale> Items { get; set; }
        public ListSaleResult(int totalSize, List<Sale> items)
        {
            TotalSize = totalSize;
            Items = items;
        }
    }
}
