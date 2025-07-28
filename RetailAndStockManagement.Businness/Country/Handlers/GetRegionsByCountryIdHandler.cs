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
    public class GetRegionsByCountryIdHandler : IRequestHandler<GetRegionsByCountryIdRequest, List<GetRegionsByCountryIdModel>>
    {
        private readonly RetailAndStockManagementContext _context;

        public GetRegionsByCountryIdHandler(RetailAndStockManagementContext context)
        {
            _context = context;
        }

        public async Task<List<GetRegionsByCountryIdModel>> Handle(GetRegionsByCountryIdRequest request, CancellationToken cancellationToken)
        {
            return _context.Regions
                .Where(r => r.CountryId == request.CountryId)
                .Select(r => new GetRegionsByCountryIdModel
                {
                    RegionId = r.RegionId,
                    RegionName = r.RegionName
                }).ToList();
        }
    }
}