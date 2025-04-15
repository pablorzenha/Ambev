using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleItemCommandDto, SaleItem>();
            CreateMap<CreateSaleItemResultDto, CreateSaleItemReponse>();
            CreateMap<CreateSaleCommand, Sale>();
            CreateMap<CreateSaleResult, CreateSaleResponse>();
            CreateMap<CreateSaleItemRequest, CreateSaleItemCommandDto>();
            CreateMap<CreateSaleRequest, CreateSaleCommand>();

        }
    }
}
