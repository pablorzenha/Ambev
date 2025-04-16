using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.ListSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public class ListSaleProfile : Profile
    {
        public ListSaleProfile()
        {
            CreateMap<ListSaleRequest, ListSaleCommand>();
            CreateMap<Sale, GetSaleResult>();
        }
    }
}
