using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Customer.Models;
using RetailAndStockManagement.Businness.Store.Models;
using RetailAndStockManagement.Businness.Store.Requests;
using RetailAndStockManagement.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Store.Handlers;

public class GetAllStoresHandler : IRequestHandler<GetAllStoresRequest, List<GetAllStoresModel>>
{
    private readonly RetailAndStockManagementContext _context;
    public GetAllStoresHandler(RetailAndStockManagementContext context) => _context = context;


    public async Task<List<GetAllStoresModel>> Handle(GetAllStoresRequest request, CancellationToken cancellationToken)
    {
        return await _context.Stores
           .Select(x => new GetAllStoresModel
           {
               StoreId = x.StoreId,
               StoreLocation = x.StoreLocation
           }).ToListAsync();
    }


}
