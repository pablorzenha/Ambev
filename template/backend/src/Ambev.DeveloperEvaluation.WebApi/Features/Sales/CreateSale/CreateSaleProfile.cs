using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.Dtos;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleItemCommandDto, SaleItem>();
            CreateMap<CreateSaleItemResultDto, CreateSaleItemReponseDto>();

            CreateMap<CreateSaleCommand, Sale>();
            CreateMap<CreateSaleResult, CreateSaleResponse>();

            CreateMap<CreateSaleItemRequestDto, CreateSaleItemCommandDto>();
            CreateMap<CreateSaleRequest, CreateSaleCommand>();

        }
    }
}
