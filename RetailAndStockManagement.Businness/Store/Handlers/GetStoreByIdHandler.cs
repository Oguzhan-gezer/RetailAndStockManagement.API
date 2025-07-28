using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Business.Store.Models;
using RetailAndStockManagement.Business.Store.Requests;
using RetailAndStockManagement.Data.EF;
using System.Threading;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Business.Store.Handlers
{
    public class GetStoreLocationByIdHandler : IRequestHandler<GetStoreLocationByIdRequest, GetStoreLocationByIdModel>
    {
        private readonly RetailAndStockManagementContext _context;

        public GetStoreLocationByIdHandler(RetailAndStockManagementContext context)
        {
            _context = context;
        }

        public async Task<GetStoreLocationByIdModel> Handle(GetStoreLocationByIdRequest request, CancellationToken cancellationToken)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.StoreId == request.StoreId, cancellationToken);

            if (store == null)
                return null;

            return new GetStoreLocationByIdModel
            {
                StoreId = store.StoreId,
                StoreLocation = store.StoreLocation
            };
        }
    }
}
