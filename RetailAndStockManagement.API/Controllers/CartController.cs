using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Cart.Requests;
using RetailAndStockManagement.Businness.Sales.Requests;
using RetailAndStockManagement.Data.EF;
using MediatR;

namespace RetailAndStockManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly RetailAndStockManagementContext _context;
        private readonly IMediator _mediator;

        public CartController(RetailAndStockManagementContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToCart([FromBody] CreateCartItemRequest request)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId);

            if (cart == null)
            {
                cart = new CartModel
                {
                    CustomerId = request.CustomerId,
                    CreatedAt = DateTime.Now
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingItem = cart.CartItems.FirstOrDefault(i =>
                i.Barcode == request.Barcode && i.StoreId == request.StoreId);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
                existingItem.Price = request.Price;
            }
            else
            {
                var item = new CartItemModel
                {
                    CartId = cart.CartId,
                    Barcode = request.Barcode,
                    StoreId = request.StoreId,
                    Quantity = request.Quantity,
                    Price = request.Price,
                    ImageBase64 = request.ImageBase64
                };
                _context.CartItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return Ok(new { Success = true, Message = "Ürün sepete eklendi." });
        }

        [HttpGet("cart/{customerId}")]
        public async Task<IActionResult> GetCartByCustomer(int customerId)
        {
            var result = await _mediator.Send(new GetCartByCustomerRequest { CustomerId = customerId });
            return Ok(result);
        }

        [HttpDelete("remove-item/{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item == null)
                return NotFound();

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(new { Success = true, Message = "Ürün sepetten silindi." });
        }

        [HttpPost("remove-item")]
        public async Task<IActionResult> RemoveItemFromCart([FromBody] RemoveCartItemRequest request)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId);

            if (cart == null) return NotFound("Sepet bulunamadı.");

            var item = cart.CartItems.FirstOrDefault(i => i.Barcode == request.Barcode && i.StoreId == request.StoreId);
            if (item == null) return NotFound("Ürün bulunamadı.");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok("Ürün başarıyla çıkarıldı.");
        }

        [HttpPost("complete-purchase/{customerId}")]
        public async Task<IActionResult> CompletePurchase(int customerId)
        {
            var result = await _mediator.Send(new CompletePurchaseRequest { CustomerId = customerId });

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}