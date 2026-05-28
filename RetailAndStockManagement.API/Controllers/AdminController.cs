using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailAndStockManagement.Businness.User.Requests;

namespace RetailAndStockManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersRequest());
            return Ok(result);
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            if (id != request.Id) return BadRequest();
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserRequest { Id = id });
            if (!result) return NotFound();
            return Ok(new { Success = true });
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] RetailAndStockManagement.Businness.Product.Requests.CreateProductRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(new { Success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("products/{barcode}")]
        public async Task<IActionResult> UpdateProduct(string barcode, [FromBody] RetailAndStockManagement.Businness.Product.Requests.UpdateProductRequest request)
        {
            if (barcode != request.Barcode) return BadRequest();
            try
            {
                var result = await _mediator.Send(request);
                return Ok(new { Success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("products/{barcode}")]
        public async Task<IActionResult> DeleteProduct(string barcode)
        {
            var result = await _mediator.Send(new RetailAndStockManagement.Businness.Product.Requests.DeleteProductRequest { Barcode = barcode });
            if (!result) return NotFound();
            return Ok(new { Success = true });
        }

        [HttpPost("product-store")]
        public async Task<IActionResult> AssignProductToStore([FromBody] RetailAndStockManagement.Businness.Product.Requests.AssignProductToStoreRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(new { Success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
