using Ambev.DeveloperEvaluation.Application.Sales.Services.Interfaces;
using Ambev.DeveloperEvaluation.Application.Validation;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handler for processing CreateSaleCommand requests.
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleHandler> _logger;
        private readonly ISaleService _saleService;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler.
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="logger">Logger for publishing internal events</param>
        public CreateSaleHandler(
            IMapper mapper,
            ISaleService saleService,
            ILogger<CreateSaleHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _saleService = saleService;
        }

        /// <summary>
        /// Handles the CreateSaleCommand request.
        /// </summary>
        /// <param name="command">The CreateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of sale creation</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            await CommandValidator.ValidateAsync(command, validator, cancellationToken);
            
            var createdSale = await _saleService.CreateAsync(command, cancellationToken);

            _logger.LogInformation("Event: SaleCreated | SaleId: {SaleId} | Amount: {Amount}", createdSale.Id, createdSale.TotalAmount);
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }
    }
}
