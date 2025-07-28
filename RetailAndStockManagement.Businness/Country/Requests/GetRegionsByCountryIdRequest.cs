using MediatR;
using RetailAndStockManagement.Businness.Country.Models;
using System.Collections.Generic;

namespace RetailAndStockManagement.Businness.Country.Requests
{
    public class GetRegionsByCountryIdRequest : IRequest<List<GetRegionsByCountryIdModel>>
    {
        public int CountryId { get; set; }
    }
}