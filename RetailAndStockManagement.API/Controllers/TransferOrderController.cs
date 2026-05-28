using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailAndStockManagement.Businness.TransferOrder.Requests;
using System.Security.Claims;

namespace RetailAndStockManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "StoreManager,Admin")]
    public class TransferOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransferOrderController(IMediator mediator) => _mediator = mediator;

        private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        private int? GetStoreId() 
        {
            var claim = User.FindFirst("storeId");
            return claim != null ? int.Parse(claim.Value) : null;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransferOrderRequest request)
        {
            var storeId = GetStoreId();
            if (!storeId.HasValue) return BadRequest("Kullanıcının mağazası yok.");
            
            request.TargetStoreId = storeId.Value;
            request.CreatedByUserId = GetUserId();
            
            try {
                var result = await _mediator.Send(request);
                return Ok(result);
            } catch(Exception ex) {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("incoming")]
        public async Task<IActionResult> GetIncoming()
        {
            var storeId = GetStoreId();
            if (!storeId.HasValue) return BadRequest("Kullanıcının mağazası yok.");
            
            var result = await _mediator.Send(new GetMyIncomingOrdersRequest { StoreId = storeId.Value });
            return Ok(result);
        }

        [HttpGet("outgoing")]
        public async Task<IActionResult> GetOutgoing()
        {
            var storeId = GetStoreId();
            if (!storeId.HasValue) return BadRequest("Kullanıcının mağazası yok.");
            
            var result = await _mediator.Send(new GetMyOutgoingOrdersRequest { StoreId = storeId.Value });
            return Ok(result);
        }
    }
}
