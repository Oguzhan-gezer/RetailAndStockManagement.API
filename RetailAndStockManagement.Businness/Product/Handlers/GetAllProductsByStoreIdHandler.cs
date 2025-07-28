using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Product.Models;
using RetailAndStockManagement.Businness.Product.Requests;
using RetailAndStockManagement.Businness.Stock.Models;
using RetailAndStockManagement.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Product.Handlers;

public class GetAllProductsByStoreIdHandler : IRequestHandler<GetAllProductsByStoreIdRequest, List<GetAllProductsByStoreIdModel>>

{
    private readonly RetailAndStockManagementContext _context;
    public GetAllProductsByStoreIdHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<List<GetAllProductsByStoreIdModel>> Handle(GetAllProductsByStoreIdRequest request, CancellationToken cancellationToken)
    {
        return await _context.ProductStores
            .Include(x => x.Product)
            .Where(x => x.StoreId == request.StoreId)
            .Select(x => new GetAllProductsByStoreIdModel
            {
                Barcode = x.Barcode,
                ImageBase64 = x.Product.ImageBase64, 
                OptionCount = x.OptionCount,
                ProductCode = x.Product.ProductCode,
                Price = x.Product.Price
            })
            .ToListAsync(cancellationToken);
    }
}