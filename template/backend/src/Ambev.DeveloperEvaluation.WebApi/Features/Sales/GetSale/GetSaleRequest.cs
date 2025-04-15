namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Represents a request to get sale in the system.
    /// </summary>
    public class GetSaleRequest
    {
        /// <summary>
        /// Gets or sets unique identifier the sale.
        /// </summary>
        public Guid Id { get; set; }
    }
}
