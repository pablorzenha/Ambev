using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.Validation;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.GetSale
{
    /// <summary>
    /// Handler for processing GetSaleHandler requests.
    /// </summary>
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetSaleHandler
        /// </summary>
        /// <param name="saleService">The Sale service</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public GetSaleHandler(
            IMapper mapper, ISaleService saleService)
        {
            _mapper = mapper;
            _saleService = saleService;
        }

        /// <summary>
        /// Handles the Sale request
        /// </summary>
        /// <param name="command">The GetSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale details if found</returns>
        public async Task<GetSaleResult> Handle(GetSaleCommand command, CancellationToken cancellationToken)
        {
            await CommandValidation.ValidateAsync(command, new GetSaleValidator(), cancellationToken);
            var sale = await _saleService.GetByIdAsync(command.Id, cancellationToken);

            return _mapper.Map<GetSaleResult>(sale);
        }
    }
}
