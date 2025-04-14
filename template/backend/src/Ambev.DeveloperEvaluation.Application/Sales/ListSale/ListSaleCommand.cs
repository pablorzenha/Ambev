using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class ListSaleCommand : IRequest<List<GetSaleResult>>
    {
        public ListSaleCommand()
        {
            
        }
    }
}
