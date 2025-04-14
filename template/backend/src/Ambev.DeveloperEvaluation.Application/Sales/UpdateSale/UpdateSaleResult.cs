using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.Dtos;
using Ambev.DeveloperEvaluation.Domain.Enums;


namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public Guid CustomerId { get; set; }

        public Guid BranchId { get; set; }

        public List<UpdateSaleItemResultDto> Items { get; set; } = new();

        public SaleStatus Status { get; set; }
    }
}
