using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale.Dtos;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; }

        public string SaleNumber { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public Guid CustomerId { get; set; }

        public Guid BranchId { get; set; }

        public List<UpdateSaleItemCommandDto> Items { get; set; } = new();

        public SaleStatus Status { get; set; }

    }
}
