using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Defines the contract for representing a sale in the system.
    /// </summary>
    public interface ISale
    {
        /// <summary>
        /// Gets the unique identifier for the sale.
        /// </summary>
        /// <returns>The sale ID as a string.</returns>ca
        string Id { get; }

        /// <summary>
        /// Gets the sale number.
        /// </summary>
        /// <returns>The sale ID as a string.</returns>
        string SaleNumber { get; }

        /// <summary>
        /// Gets the customer unique identifier for the sale.
        /// </summary>
        /// <returns>The sale ID as a string.</returns>
        string CustomerId { get; }

        /// <summary>
        /// Gets the customer unique identifier for the sale.
        /// </summary>
        /// <returns>The sale ID as a string.</returns>
        string BranchId { get; }

        /// <summary>
        /// Gets the total amount of the sale, calculated from the sale items.
        /// </summary>
        /// <returns>The total amount of the sale.</returns>
        decimal TotalAmount { get; }

        /// <summary>
        /// Gets the date and time when the sale was made.
        /// </summary>
        /// <returns>The sale date.</returns>
        DateTime Date { get; }

        /// <summary>
        /// Gets the date and time when the sale was created.
        /// </summary>
        /// <returns>The creation date of the sale.</returns>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the date and time of the last update to the sale.
        /// </summary>
        /// <returns>The last update date.</returns>
        DateTime? UpdatedAt { get; }

        string? Status { get; }

        /// <summary>
        /// Gets the list of items associated with the sale.
        /// </summary>
        /// <returns>A list of sale items.</returns>
        List<ISaleItem> Items { get; }

        /// <summary>
        /// Calculates the total value of the sale based on the items.
        /// </summary>
        void CalculateTotal();

        void AddItem(Guid productId, int quantity, decimal unitPrice);

        /// <summary>
        /// Adds a new item to the sale.
        /// </summary>
        /// <param name="item">The item to be added to the sale.</param>
        //void AddItem(ISale item);

    }
}
