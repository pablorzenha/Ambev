using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Sales.GetSale
{
    public class GetSaleCommand : IRequest<GetSaleResult>
    {
        /// <summary>
        /// The unique identifier of the sale to retrieve
        /// </summary>
        public Guid Id { get; set; }


        public GetSaleCommand() { }
        /// <summary>
        /// Initializes a new instance of GetSaleCommand
        /// </summary>
        /// <param name="id">The ID of the sale to retrieve</param>
        public GetSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
