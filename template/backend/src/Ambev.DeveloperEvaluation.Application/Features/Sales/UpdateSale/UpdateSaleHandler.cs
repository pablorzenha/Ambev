using Ambev.DeveloperEvaluation.Application.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Application.Validation;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Sales.UpdateSale
{
    /// <summary>
    /// Handler for processing UpdateSaleHandler requests.
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of UpdateSaleHandler.
        /// </summary>
        /// <param name="saleService">The sale service</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public UpdateSaleHandler(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the CreateSaleCommand request.
        /// </summary>
        /// <param name="command">The CreateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of sale to update</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            await CommandValidation.ValidateAsync(command, new UpdateSaleValidator(), cancellationToken);

            var sale = await _saleService.UpdateAsync(command, cancellationToken);

            return _mapper.Map<UpdateSaleResult>(sale);
        }
    }
}
