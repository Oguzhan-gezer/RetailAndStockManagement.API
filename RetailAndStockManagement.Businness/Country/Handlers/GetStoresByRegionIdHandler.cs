using MediatR;
using RetailAndStockManagement.Businness.Country.Models;
using RetailAndStockManagement.Businness.Country.Requests;
using RetailAndStockManagement.Data.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Country.Handlers
{
    public class GetStoresByRegionIdHandler : IRequestHandler<GetStoresByRegionIdRequest, List<GetStoresByRegionIdModel>>
    {
        private readonly RetailAndStockManagementContext _context;

        public GetStoresByRegionIdHandler(RetailAndStockManagementContext context)
        {
            _context = context;
        }

        public async Task<List<GetStoresByRegionIdModel>> Handle(GetStoresByRegionIdRequest request, CancellationToken cancellationToken)
        {
            return _context.Stores
                .Where(s => s.RegionId == request.RegionId)
                .Select(s => new GetStoresByRegionIdModel
                {
                    StoreId = s.StoreId,
                    StoreLocation = s.StoreLocation
                }).ToList();
        }
    }
}