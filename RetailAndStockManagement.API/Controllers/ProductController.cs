using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailAndStockManagement.Businness.Product.Models;
using RetailAndStockManagement.Businness.Product.Requests;

namespace RetailAndStockManagement.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;
       
        [HttpGet("getAllByStoreId")]
        public async Task<IActionResult> GetAllByStoreId([FromQuery] GetAllProductsByStoreIdRequest query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("get-properties-by-barcode/{barcode}")]
        public async Task<ActionResult<GetProductPropertiesByBarcodeModel>> GetProductPropertiesByBarcode(string barcode)
        {
            var result = await _mediator.Send(new GetProductPropertiesByBarcodeRequest { Barcode = barcode });

            if (result == null)
                return NotFound("Ürün bulunamadı.");

            return Ok(result);
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsRequest());
            return Ok(result);
        }


    }
}
