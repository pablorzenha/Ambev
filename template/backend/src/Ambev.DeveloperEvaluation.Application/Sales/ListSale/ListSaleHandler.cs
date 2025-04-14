using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.Services.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class ListSaleHandler : IRequestHandler<ListSaleCommand, ListSaleResult>
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of ListSaleHandler
        /// </summary>
        /// <param name="saleRepository">The Saçe repository</param>
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
        /// <param name="request">The ListSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale details if found</returns>
        public async Task<ListSaleResult> Handle(ListSaleCommand request, CancellationToken cancellationToken)
        {
            var sales = await _saleService.GetAllAsync(request, cancellationToken);

            return _mapper.Map<ListSaleResult>(sales);
        }
    }
}

