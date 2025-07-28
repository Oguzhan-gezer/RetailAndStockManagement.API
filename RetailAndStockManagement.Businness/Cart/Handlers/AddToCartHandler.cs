using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Data.EF;

public class AddToCartHandler : IRequestHandler<AddToCartRequest, AddToCartModel>
{
    private readonly RetailAndStockManagementContext _context;

    public AddToCartHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<AddToCartModel> Handle(AddToCartRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _context.Products.FindAsync(request.Barcode);
            if (product == null)
                return new AddToCartModel { IsSuccess = false, Message = "Ürün bulunamadı." };

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId, cancellationToken);
            if (cart == null)
            {
                cart = new CartModel { CustomerId = request.CustomerId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync(cancellationToken);
            }

            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(x => x.CartId == cart.CartId && x.Barcode == request.Barcode && x.StoreId == request.StoreId);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
                _context.CartItems.Update(existingItem);
            }
            else
            {
                var productStore = await _context.ProductStores
                    .FirstOrDefaultAsync(p => p.Barcode == request.Barcode && p.StoreId == request.StoreId);

                _context.CartItems.Add(new CartItemModel
                {
                    CartId = cart.CartId,
                    Barcode = request.Barcode,
                    StoreId = request.StoreId,
                    Quantity = request.Quantity,
                    Price = product.Price,
                    ImageBase64 = product.ImageBase64
                });
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new AddToCartModel { IsSuccess = true, Message = "Ürün sepete eklendi." };
        }
        catch (Exception ex)
        {
            return new AddToCartModel { IsSuccess = false, Message = $"Hata: {ex.Message}" };
        }
    }
}