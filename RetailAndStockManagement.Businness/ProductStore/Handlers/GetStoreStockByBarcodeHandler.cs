using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Stock.Models;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.Stock.Handlers
{
    public class GetStoreStockByBarcodeHandler
        : IRequestHandler<GetStoreStockByBarcodeRequest, List<StoreStockModel>>
    {
        private readonly RetailAndStockManagementContext _context;

        public GetStoreStockByBarcodeHandler(RetailAndStockManagementContext context)
        {
            _context = context;
        }

        public async Task<List<StoreStockModel>> Handle(GetStoreStockByBarcodeRequest request, CancellationToken cancellationToken)
        {
            var result = await (
                from ps in _context.ProductStores
                join s in _context.Stores on ps.StoreId equals s.StoreId
                where ps.Barcode == request.Barcode
                select new StoreStockModel
                {
                    StoreLocation = s.StoreLocation,
                    OptionCount = ps.OptionCount
                }
            ).ToListAsync(cancellationToken);

            return result;
        }
    }
}
