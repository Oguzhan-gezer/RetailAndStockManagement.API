using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Country.Models;
using RetailAndStockManagement.Businness.Country.Requests;
using RetailAndStockManagement.Data.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Country.Handlers
{
    public class GetAllCountriesHandler : IRequestHandler<GetAllCountriesRequest, List<GetAllCountriesModel>>
    {
        private readonly RetailAndStockManagementContext _context;

        public GetAllCountriesHandler(RetailAndStockManagementContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllCountriesModel>> Handle(GetAllCountriesRequest request, CancellationToken cancellationToken)
        {
            return await _context.Countries
                .Select(c => new GetAllCountriesModel
                {
                    CountryId = c.CountryId,
                    CountryName = c.CountryName
                }).ToListAsync();
        }
    }
}