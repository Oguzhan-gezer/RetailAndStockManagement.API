using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Product.Models;
using RetailAndStockManagement.Businness.Product.Requests;
using RetailAndStockManagement.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Product.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, List<ProductItemModel>>
{
    private readonly RetailAndStockManagementContext _context;
    public GetAllProductsHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<List<ProductItemModel>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .Select(p => new ProductItemModel
            {
                Barcode = p.Barcode,
                Price = p.Price,
                ImageBase64 = p.ImageBase64,
                ProductCode = p.ProductCode,
                ProductProperties = p.ProductProperties
            })
            .ToListAsync(cancellationToken);
    }

}
