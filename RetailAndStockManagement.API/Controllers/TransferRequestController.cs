using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailAndStockManagement.Businness.TransferRequest.Requests;
using System.Security.Claims;

namespace RetailAndStockManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "StoreManager,Admin")]
    public class TransferRequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransferRequestController(IMediator mediator) => _mediator = mediator;

        private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        private int? GetStoreId() 
        {
            var claim = User.FindFirst("storeId");
            return claim != null ? int.Parse(claim.Value) : null;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransferRequestRequest request)
        {
            var storeId = GetStoreId();
            if (!storeId.HasValue) return BadRequest("Kullanıcının mağazası yok.");
            
            request.SourceStoreId = storeId.Value;
            request.CreatedByUserId = GetUserId();
            
            try {
                var result = await _mediator.Send(request);
                return Ok(result);
            } catch(Exception ex) {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("my-requests")]
        public async Task<IActionResult> GetMyRequests()
        {
            var storeId = GetStoreId();
            if (!storeId.HasValue) return BadRequest("Kullanıcının mağazası yok.");
            
            var result = await _mediator.Send(new GetMyTransferRequestsRequest { StoreId = storeId.Value });
            return Ok(result);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            try {
                var result = await _mediator.Send(new CancelTransferRequestRequest { RequestId = id, UserId = GetUserId() });
                if (!result) return NotFound();
                return Ok(new { Success = true });
            } catch(Exception ex) {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? barcode, [FromQuery] string? keyword)
        {
            var storeId = GetStoreId();
            var searchTerm = keyword ?? barcode ?? string.Empty;
            var result = await _mediator.Send(new SearchTransferRequestsByBarcodeRequest { 
                Barcode = searchTerm, 
                ExcludeStoreId = storeId 
            });
            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            var storeId = GetStoreId();
            var result = await _mediator.Send(new GetAllActiveTransferRequestsRequest { ExcludeStoreId = storeId });
            return Ok(result);
        }
    }
}
