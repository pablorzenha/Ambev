using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.ListSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public class ListSaleProfile : Profile
    {
        public ListSaleProfile()
        {
            CreateMap<ListSaleRequest, ListSaleCommand>();
        }
    }
}
