using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Validation;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of SalesController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new sale
        /// </summary>
        /// <param name="request">The sale creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created sale details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            await RequestValidator.ValidateAsync(request, new CreateSaleRequestValidator(), cancellationToken);

            var command = _mapper.Map<CreateSaleCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = _mapper.Map<CreateSaleResponse>(result)
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetSaleRequest { Id = id };
            await RequestValidator.ValidateAsync(request, new GetSaleRequestValidator(), cancellationToken);

            var command = _mapper.Map<GetSaleCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Ok( _mapper.Map<GetSaleResponse>(result) );
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<GetSaleResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSales(
                [FromQuery(Name = "_page")] int skip = 1,
                [FromQuery(Name = "_size")] int take = 10,
                [FromQuery(Name = "_order")] string? order = null,
                CancellationToken cancellationToken = default
                )
        {
            var request = new ListSaleRequest
            {
                Skip = skip == 0 ? 1 :skip,
                Take = take,
                Order = order
            };
            await RequestValidator.ValidateAsync(request, new ListSaleRequestValidator(), cancellationToken);

            var command = _mapper.Map<ListSaleCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            var items = _mapper.Map<List<GetSaleResult>>(result.Items);
            var pagineted = new PaginatedList<GetSaleResult>(items, result.TotalSize, command.Skip, command.Take);
            return OkPaginated(pagineted) ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteSaleRequest { Id = id };
            await RequestValidator.ValidateAsync(request, new DeleteSaleRequestValidator(), cancellationToken);

            var command = _mapper.Map<DeleteSaleCommand>(request);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSale([FromRoute] Guid id,[FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            if(id != request.Id)
                return BadRequest();

            await RequestValidator.ValidateAsync(request, new UpdateSaleValidator(), cancellationToken);

            var command = _mapper.Map<UpdateSaleCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<UpdateSaleResponse>(result));
        }
    }
}
