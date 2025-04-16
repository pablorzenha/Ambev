using Ambev.DeveloperEvaluation.Application.UseCases.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.ListSale;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Interfaces
{
    public interface ISaleService
    {
        Task<Sale> CreateAsync(CreateSaleCommand command, CancellationToken cancellationToken);
        Task<Sale?> DeleteAsync(DeleteSaleCommand command, CancellationToken cancellationToken);
        Task<Sale> GetByIdAsync(Guid saleId, CancellationToken cancellationToken);
        Task<List<Sale>> GetAllAsync(ListSaleCommand command, CancellationToken cancellationToken);
        Task<Sale> UpdateAsync(UpdateSaleCommand command, CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}
