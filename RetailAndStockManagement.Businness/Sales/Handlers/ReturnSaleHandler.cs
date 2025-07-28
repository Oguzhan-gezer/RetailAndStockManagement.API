using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Sales.Models;
using RetailAndStockManagement.Businness.Sales.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.Sales.Handlers
{
    public class ReturnSaleHandler : IRequestHandler<ReturnSaleRequest, ReturnSaleModel>
    {
        private readonly RetailAndStockManagementContext _context;

        public ReturnSaleHandler(RetailAndStockManagementContext context)
        {
            _context = context;
        }

        public async Task<ReturnSaleModel> Handle(ReturnSaleRequest request, CancellationToken cancellationToken)
        {
            var productStore = await _context.ProductStores
                .FirstOrDefaultAsync(ps =>
                    ps.Barcode == request.Barcode &&
                    ps.StoreId == request.StoreId,
                    cancellationToken);

            if (productStore == null)
            {
                return new ReturnSaleModel
                {
                    IsSuccess = false,
                    Message = "Ürün mağazada bulunamadı."
                };
            }

            productStore.OptionCount += request.Quantity;
            _context.ProductStores.Update(productStore);

            var returnSale = new SaleModel
            {
                Barcode = request.Barcode,
                StoreId = request.StoreId,
                Quantity = -Math.Abs(request.Quantity),
                SaleDate = DateTime.Now,
                CustomerId = request.CustomerId
            };

            _context.Sales.Add(returnSale);

            await _context.SaveChangesAsync(cancellationToken);

            return new ReturnSaleModel
            {
                IsSuccess = true,
                Message = "İade işlemi başarıyla kaydedildi."
            };
        }
    }
}
