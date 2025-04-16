using Ambev.DeveloperEvaluation.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Sales.ListSale
{
    public class ListSaleHandler : IRequestHandler<ListSaleCommand, ListSaleResult>
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of ListSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale service</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public ListSaleHandler(
            ISaleService saleService,
            IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the Sale request
        /// </summary>
        /// <param name="command">The ListSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale details if found</returns>
        public async Task<ListSaleResult> Handle(ListSaleCommand command, CancellationToken cancellationToken)
        {
            var list = await _saleService.GetAllAsync(command, cancellationToken);
            var count = await _saleService.CountAsync(cancellationToken);

            return new ListSaleResult(count, list);
        }
    }
}

