using Ambev.DeveloperEvaluation.Application.UseCases.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales.TestData
{
    public static class GetSaleHandlerTestData
    {
        private static readonly Faker<GetSaleCommand> GetSaleCommandFaker = new Faker<GetSaleCommand>()
           .RuleFor(c => c.Id, Guid.NewGuid());

        /// <summary>
        /// Generates a valid CreateSaleCommand.
        /// </summary>
        public static GetSaleCommand GenerateValidCommand() => GetSaleCommandFaker.Generate();
    }
}
