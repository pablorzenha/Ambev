using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<GetSaleItemResultDto, GetSaleItemResponse>();
            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<GetSaleRequest, GetSaleCommand>();
        }
    }
}
