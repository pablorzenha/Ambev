﻿using Ambev.DeveloperEvaluation.Application.UseCases.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Interfaces
{
    public interface ISaleItemService
    {
        void Update(Sale sale, List<UpdateSaleItemCommand> updatedItems);

    }
}
