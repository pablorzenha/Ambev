using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.Services.Interfaces
{
    public interface ISaleService
    {
        Task<Sale> CreateAsync(CreateSaleCommand command, CancellationToken cancellationToken);
        Task<Sale> DeleteAsync(DeleteSaleCommand command, CancellationToken cancellationToken);
        Task<Sale> GetByIdAsync(Guid saleId, CancellationToken cancellationToken);
        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken);
        Task<Sale> UpdateAsync(UpdateSaleCommand request, CancellationToken cancellationToken);
    }
}
