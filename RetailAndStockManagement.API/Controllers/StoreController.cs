using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailAndStockManagement.Business.Store.Requests;
using RetailAndStockManagement.Businness.Store.Requests;

namespace RetailAndStockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator) => _mediator = mediator;

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllStores([FromQuery] GetAllStoresRequest query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("get-store-location/{storeId}")]
        public async Task<IActionResult> GetStoreLocationById(int storeId)
        {
            var result = await _mediator.Send(new GetStoreLocationByIdRequest(storeId));
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
