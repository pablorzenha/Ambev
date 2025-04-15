namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public class ListSaleRequest
    {
        /// <summary>
        /// Gets or sets page the list sale.
        /// </summary>
        public int Skip {  get; set; }

        /// <summary>
        /// Gets or sets take elements the list sale.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets order the list sale.
        /// </summary>
        public string? Order { get; set; }
    }
}
