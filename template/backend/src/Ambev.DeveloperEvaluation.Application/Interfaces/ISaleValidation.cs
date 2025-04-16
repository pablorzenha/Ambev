using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Interfaces
{
    public interface ISaleValidation
    {
        Task<Sale> ExistsSaleIdAsync(Guid saleId, CancellationToken cancellationToken);
        Task SaleNumberIsUniqueAsync(string saleNumber, CancellationToken cancellationToken);
    }
}
