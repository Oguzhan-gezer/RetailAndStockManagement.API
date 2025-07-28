using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Sales.Models;
using RetailAndStockManagement.Businness.Sales.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.Sales.Handlers
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleRequest, CreateSaleModel>
    {
        private readonly RetailAndStockManagementContext _context;

        public CreateSaleHandler(RetailAndStockManagementContext context)
        {
            _context = context;
        }

        public async Task<CreateSaleModel> Handle(CreateSaleRequest request, CancellationToken cancellationToken)
        {
            
            var productStore = await _context.ProductStores
                .FirstOrDefaultAsync(ps =>
                    ps.Barcode == request.Barcode &&
                    ps.StoreId == request.StoreId,
                    cancellationToken);

            if (productStore == null)
            {
                return new CreateSaleModel
                {
                    IsSuccess = false,
                    Message = "Ürün mağazada bulunamadı."
                };
            }

            if (productStore.OptionCount < request.Quantity)
            {
                return new CreateSaleModel
                {
                    IsSuccess = false,
                    Message = "Yeterli stok yok."
                };
            }

           
            productStore.OptionCount -= request.Quantity;
            _context.ProductStores.Update(productStore);

           
            var sale = new SaleModel
            {
                Barcode = request.Barcode,
                StoreId = request.StoreId,
                Quantity = request.Quantity,
                SaleDate = DateTime.Now
            };

            _context.Sales.Add(sale);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateSaleModel
            {
                IsSuccess = true,
                Message = "Satış başarıyla kaydedildi."
            };
        }
    }
}
