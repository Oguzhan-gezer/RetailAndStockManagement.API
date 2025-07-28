using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailAndStockManagement.Business.Sales.Requests;
using RetailAndStockManagement.Businness.Sales.Models;
using RetailAndStockManagement.Businness.Sales.Requests;

namespace RetailAndStockManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnProduct([FromBody] ReturnSaleRequest request)
        {
            var result = await _mediator.Send(request);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("also-bought/{barcode}")]
        public async Task<IActionResult> GetAlsoBoughtProducts(string barcode)
        {
            var result = await _mediator.Send(new GetAlsoBoughtProductsRequest(barcode));
            return Ok(result);
        }

    }
}
