using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Data.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GetCartByCustomerHandler : IRequestHandler<GetCartByCustomerRequest, GetCartByCustomerModel>
{
    private readonly RetailAndStockManagementContext _context;

    public GetCartByCustomerHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<GetCartByCustomerModel> Handle(GetCartByCustomerRequest request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId, cancellationToken);

        if (cart == null || cart.CartItems.Count == 0)
        {
            return new GetCartByCustomerModel
            {
                Items = new List<CartItemModel>(),
                TotalPrice = 0
            };
        }

        var barcodes = cart.CartItems.Select(i => i.Barcode).Distinct().ToList();

  
        var productMap = await _context.Products
            .Where(p => barcodes.Contains(p.Barcode))
            .ToDictionaryAsync(p => p.Barcode, cancellationToken);

        foreach (var item in cart.CartItems)
        {
            if (productMap.TryGetValue(item.Barcode, out var product))
            {
                item.ImageBase64 = product.ImageBase64;
                item.Price = product.Price;
            }
        }

        var total = cart.CartItems.Sum(i => i.Price * i.Quantity);

        return new GetCartByCustomerModel
        {
            Items = cart.CartItems.Select(ci => new CartItemModel
            {
                Barcode = ci.Barcode,
                Quantity = ci.Quantity,
                Price = ci.Price,
                ImageBase64 = ci.ImageBase64,
                StoreId = ci.StoreId
            }).ToList(),
            TotalPrice = total
        };
    }
}
