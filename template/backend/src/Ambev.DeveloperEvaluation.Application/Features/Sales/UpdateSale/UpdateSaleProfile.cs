using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<Sale, UpdateSaleResult>();
            CreateMap<SaleItem, UpdateSaleItemResult>();
        }
    }
}
