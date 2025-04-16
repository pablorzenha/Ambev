using Ambev.DeveloperEvaluation.Application.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Features.Sales.ListSale;
using Ambev.DeveloperEvaluation.Application.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Interfaces
{
    public interface ISaleService
    {
        Task<Sale> CreateAsync(CreateSaleCommand command, CancellationToken cancellationToken);
        Task<Sale> DeleteAsync(DeleteSaleCommand command, CancellationToken cancellationToken);
        Task<Sale> GetByIdAsync(Guid saleId, CancellationToken cancellationToken);
        Task<ListSaleResult> GetAllAsync(ListSaleCommand request, CancellationToken cancellationToken);
        Task<Sale> UpdateAsync(UpdateSaleCommand request, CancellationToken cancellationToken);
    }
}
