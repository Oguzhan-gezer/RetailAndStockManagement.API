using MediatR;
using RetailAndStockManagement.Businness.Country.Models;
using System.Collections.Generic;

namespace RetailAndStockManagement.Businness.Country.Requests
{
    public class GetStoresByRegionIdRequest : IRequest<List<GetStoresByRegionIdModel>>
    {
        public int RegionId { get; set; }
    }
}