using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale.Dtos;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<GetSaleItemResultDto, GetSaleItemResponseDto>();
            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<GetSaleRequest, GetSaleCommand>();
        }
    }
}
