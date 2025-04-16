using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Validation
{
    public class SaleValidation : ISaleValidation
    {
        private readonly ISaleRepository _saleRepository;

        public SaleValidation(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Sale> ExistsSaleIdAsync(Guid saleId, CancellationToken cancellationToken)
        {
            return await _saleRepository.GetByIdAsync(saleId, cancellationToken)
                ?? throw new KeyNotFoundException($"Sale with ID {saleId} not found");
        }

        public async Task SaleNumberIsUniqueAsync(string saleNumber, CancellationToken cancellationToken)
        {
            var existingSale = await _saleRepository.GetBySaleNameAsync(saleNumber, cancellationToken);
            if (existingSale != null)
                throw new InvalidOperationException($"Sale with sale number '{saleNumber}' already exists");
        }
    }
}
