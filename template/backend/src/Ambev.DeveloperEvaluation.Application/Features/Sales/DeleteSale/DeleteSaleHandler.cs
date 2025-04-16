using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.Validation;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.DeleteSale
{
    /// <summary>
    /// Handler for processing DeleteSaleHandler requests.
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of DeleteSaleHandler
        /// </summary>
        /// <param name="saleService">The Sale Service</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public DeleteSaleHandler(
            IMapper mapper,
            ISaleService saleService)
        {
            _mapper = mapper;
            _saleService = saleService;
        }

        /// <summary>
        /// Handles the Sale request
        /// </summary>
        /// <param name="command">The DeleteSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale details if found</returns>
        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            await CommandValidation.ValidateAsync(command, new DeleteSaleValidator(), cancellationToken);
            var sale = await _saleService.DeleteAsync(command, cancellationToken);
            return _mapper.Map<DeleteSaleResult>(sale);
        }
    }
}
