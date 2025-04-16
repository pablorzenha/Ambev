using Ambev.DeveloperEvaluation.Application.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleItemCommand, SaleItem>();
            CreateMap<CreateSaleItemResult, CreateSaleItemReponse>();
            CreateMap<CreateSaleCommand, Sale>();
            CreateMap<CreateSaleResult, CreateSaleResponse>();
            CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
            CreateMap<CreateSaleRequest, CreateSaleCommand>();

        }
    }
}
