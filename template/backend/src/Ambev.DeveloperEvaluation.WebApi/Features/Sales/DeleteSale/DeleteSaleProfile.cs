﻿using AutoMapper;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.DeleteSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    public class DeleteSaleProfile : Profile
    {
        public DeleteSaleProfile()
        {
          CreateMap<DeleteSaleRequest,DeleteSaleCommand>();
        }
    }
}
