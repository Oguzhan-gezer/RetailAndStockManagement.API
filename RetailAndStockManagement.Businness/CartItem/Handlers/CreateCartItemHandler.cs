using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Sales.Requests;
using RetailAndStockManagement.Data.EF;
public class CreateCartItemHandler : IRequestHandler<CreateCartItemRequest, CreateCartItemModel>
{
    private readonly RetailAndStockManagementContext _context;

    public CreateCartItemHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<CreateCartItemModel> Handle(CreateCartItemRequest request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId, cancellationToken);

        if (cart == null)
        {
            cart = new CartModel
            {
                CustomerId = request.CustomerId,
                CreatedAt = DateTime.Now,
                CartItems = new List<CartItemModel>()
            };
            await _context.Carts.AddAsync(cart, cancellationToken);
        }

        var existingItem = cart.CartItems.FirstOrDefault(i =>
            i.Barcode == request.Barcode && i.StoreId == request.StoreId);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            var productStore = await _context.ProductStores
                .FirstOrDefaultAsync(ps => ps.Barcode == request.Barcode && ps.StoreId == request.StoreId, cancellationToken);

            if (productStore == null)
            {
                return new CreateCartItemModel { IsSuccess = false, Message = "Ürün mağazada bulunamadı." };
            }

            var product = await _context.Products.FindAsync(request.Barcode);
            if (product == null)
            {
                return new CreateCartItemModel { IsSuccess = false, Message = "Ürün bulunamadı." };
            }

            var newItem = new CartItemModel
            {
                Barcode = request.Barcode,
                StoreId = request.StoreId,
                Quantity = request.Quantity,
                Price = product.Price,
                ImageBase64 = product.ImageBase64
            };

            cart.CartItems.Add(newItem);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCartItemModel { IsSuccess = true, Message = "Ürün sepete eklendi." };
    }
}