using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Repository implementation for managing sales using EF Core.
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of SaleRepository.
        /// </summary>
        /// <param name="context">The database context</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        /// <inheritdoc/>
        public async Task<Sale?> GetBySaleNameAsync(string saleNumber, CancellationToken cancellationToken)
        {
            return await _context.Sales
                .FirstOrDefaultAsync(s => s.SaleNumber.Equals(saleNumber), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Sale?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Sales
                .FirstOrDefaultAsync(s => s.Id.Equals(Id), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .ToListAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);

        }
        /// <inheritdoc/>
        public async Task DeleteAsync(Sale sale, CancellationToken cancellationToken)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
