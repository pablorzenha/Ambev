using Ambev.DeveloperEvaluation.Application.UseCases.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.UseCases.Sales.GetSale;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.UseCases.Sales.TestData
{
    public static class DeleteSaleHandlerTestData
    {
        private static readonly Faker<DeleteSaleCommand> DeleteSaleCommandFaker = new Faker<DeleteSaleCommand>()
         .RuleFor(c => c.Id, Guid.NewGuid());

        /// <summary>
        /// Generates a valid CreateSaleCommand.
        /// </summary>
        public static DeleteSaleCommand GenerateValidCommand() => DeleteSaleCommandFaker.Generate();
    }
}
