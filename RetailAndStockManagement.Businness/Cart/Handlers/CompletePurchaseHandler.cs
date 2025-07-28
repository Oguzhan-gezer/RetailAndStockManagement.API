using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Cart.Models;
using RetailAndStockManagement.Businness.Cart.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.Cart.Handlers
{
    public class CompletePurchaseHandler : IRequestHandler<CompletePurchaseRequest, CompletePurchaseModel>
    {
        private readonly RetailAndStockManagementContext _context;

        public CompletePurchaseHandler(RetailAndStockManagementContext context)
        {
            _context = context;
        }

        public async Task<CompletePurchaseModel> Handle(CompletePurchaseRequest request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId, cancellationToken);

            if (cart == null || !cart.CartItems.Any())
            {
                return new CompletePurchaseModel { IsSuccess = false, Message = "Sepet boş." };
            }

            foreach (var item in cart.CartItems)
            {
                var productStore = await _context.ProductStores
                    .FirstOrDefaultAsync(ps => ps.Barcode == item.Barcode && ps.StoreId == item.StoreId);

                if (productStore == null || productStore.OptionCount < item.Quantity)
                {
                    return new CompletePurchaseModel { IsSuccess = false, Message = $"Stok yetersiz: {item.Barcode}" };
                }

                productStore.OptionCount -= item.Quantity;
                _context.ProductStores.Update(productStore);

                _context.Sales.Add(new SaleModel
                {
                    Barcode = item.Barcode,
                    StoreId = item.StoreId,
                    Quantity = item.Quantity,
                    SaleDate = DateTime.Now,
                    CustomerId = request.CustomerId
                });
            }

            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync(cancellationToken);

            return new CompletePurchaseModel
            {
                IsSuccess = true,
                Message = "Satın alma işlemi başarıyla tamamlandı."
            };
        }
    }
}
