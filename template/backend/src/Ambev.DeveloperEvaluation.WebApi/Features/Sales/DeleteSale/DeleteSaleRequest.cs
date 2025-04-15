namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    /// <summary>
    /// Represents a request to delete sale in the system.
    /// </summary>
    public class DeleteSaleRequest
    {
        /// <summary>
        /// Gets or sets unique identifier the sale.
        /// </summary>
        public Guid Id { get; set; }
    }
}
