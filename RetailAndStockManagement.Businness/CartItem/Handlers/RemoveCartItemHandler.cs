using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Cart.Models;
using RetailAndStockManagement.Businness.Cart.Requests;
using RetailAndStockManagement.Data.EF;
using System.Threading;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Cart.Handlers
{
    public class RemoveCartItemHandler : IRequestHandler<RemoveCartItemRequest, RemoveCartItemModel>
    {
        private readonly RetailAndStockManagementContext _context;

        public RemoveCartItemHandler(RetailAndStockManagementContext context)
        {
            _context = context;
        }

        public async Task<RemoveCartItemModel> Handle(RemoveCartItemRequest request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId, cancellationToken);

            if (cart == null)
            {
                return new RemoveCartItemModel
                {
                    IsSuccess = false,
                    Message = "Sepet bulunamadı."
                };
            }

            var item = cart.CartItems.FirstOrDefault(i => i.Barcode == request.Barcode && i.StoreId == request.StoreId);
            if (item == null)
            {
                return new RemoveCartItemModel
                {
                    IsSuccess = false,
                    Message = "Ürün sepet içinde bulunamadı."
                };
            }

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);

            return new RemoveCartItemModel
            {
                IsSuccess = true,
                Message = "Ürün başarıyla sepetten çıkarıldı."
            };
        }
    }
}