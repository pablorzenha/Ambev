using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Interface for managing sales persistence.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Persists a new sale to the data store.
        /// </summary>
        /// <param name="sale">The sale entity to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale entity</returns>
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The unique sale identifier</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale entity or null if not found</returns>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The unique sale identifier</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale entity or null if not found</returns>
        Task<Sale?> GetBySaleNameAsync(string saleName, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all sales from the data store.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Enumerable list of sales</returns>
        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken);

        Task DeleteAsync(Sale sale, CancellationToken cancellationToken);
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken);
        Task<List<Sale>> GetAllPageAsync(int skip, int take, string? order, CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);


    }
}
