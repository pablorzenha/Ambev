using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Sales.DeleteSale
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        /// <summary>
        /// The unique identifier of the sale to retrieve
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of DeleteSaleCommand
        /// </summary>
        /// <param name="id">The ID of the sale to retrieve</param>
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
