﻿using Ambev.DeveloperEvaluation.Application.UseCases.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();

            CreateMap<UpdateSaleResult, UpdateSaleResponse>();
            CreateMap<UpdateSaleItemResult, UpdateSaleItemsResponse>();

        }
    }
}
