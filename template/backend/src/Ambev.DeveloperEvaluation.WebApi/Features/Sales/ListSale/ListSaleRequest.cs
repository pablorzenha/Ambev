namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public class ListSaleRequest
    {
        public int Skip {  get; set; }
        public int Take { get; set; }
        public string? Order { get; set; }
    }
}
