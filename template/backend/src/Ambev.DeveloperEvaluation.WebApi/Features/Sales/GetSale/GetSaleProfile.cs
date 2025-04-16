using AutoMapper;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<GetSaleItemResult, GetSaleItemResponse>();
            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<GetSaleRequest, GetSaleCommand>();
        }
    }
}
