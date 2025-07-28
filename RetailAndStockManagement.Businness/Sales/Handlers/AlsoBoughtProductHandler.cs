using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Business.Sales.Models;
using RetailAndStockManagement.Business.Sales.Requests;
using RetailAndStockManagement.Data.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GetAlsoBoughtProductsHandler : IRequestHandler<GetAlsoBoughtProductsRequest, List<AlsoBoughtProductModel>>
{
    private readonly RetailAndStockManagementContext _context;

    public GetAlsoBoughtProductsHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<List<AlsoBoughtProductModel>> Handle(GetAlsoBoughtProductsRequest request, CancellationToken cancellationToken)
    {
        // 1. Bu ürünü alan customerId'leri bul
        var saleGroups = await _context.Sales
            .Where(s => s.Barcode == request.Barcode)
            .Select(s => s.CustomerId)
            .Distinct()
            .ToListAsync(cancellationToken);

        // 2. Bu customer'ların sepetindeki ürünleri al
        var cartItemBarcodes = await _context.Carts
            .Where(c => saleGroups.Contains(c.CustomerId))
            .SelectMany(c => c.CartItems.Select(ci => ci.Barcode))
            .Distinct()
            .ToListAsync(cancellationToken);

        // 2.1. Aktif kullanıcının sepetindeki ürünleri de al
        var currentUserCartBarcodes = await _context.Carts
            .Where(c => c.CustomerId == request.CustomerId)
            .SelectMany(c => c.CartItems.Select(ci => ci.Barcode))
            .Distinct()
            .ToListAsync(cancellationToken);

        // 2.2. İki listeyi birleştir (tümünü filtrelemek için)
        cartItemBarcodes.AddRange(currentUserCartBarcodes);
        cartItemBarcodes = cartItemBarcodes.Distinct().ToList();

        // 3. Aynı customer'ların aldığı diğer ürünleri bul
        var otherSales = await _context.Sales
            .Where(s => saleGroups.Contains(s.CustomerId) && s.Quantity > 0)
            .GroupBy(s => s.Barcode)
            .Select(g => new
            {
                Barcode = g.Key,
                Total = g.Sum(x => x.Quantity)
            })
            .OrderByDescending(x => x.Total)
            .Take(20)
            .ToListAsync(cancellationToken);

        // 4. Sepette olanları çıkar
        var filteredBarcodes = otherSales
            .Where(x => !cartItemBarcodes.Contains(x.Barcode))
            .Select(x => x.Barcode)
            .ToList();

        var productInfos = await _context.ProductStores
    .Where(ps => filteredBarcodes.Contains(ps.Barcode) && ps.OptionCount > 0)
    .Include(ps => ps.Product) 
    .Select(ps => new
    {
        ps.Barcode,
        ps.Product.ProductCode,
        ps.Product.ImageBase64,
        ps.Product.Price,
        ps.OptionCount,
        ps.StoreId
    })
    .ToListAsync(cancellationToken);

        var result = productInfos.Select(p => new AlsoBoughtProductModel
        {
            Barcode = p.Barcode.ToString(),
            ProductCode = p.ProductCode,
            ImageBase64 = p.ImageBase64,
            Price = p.Price,
            OptionCount = p.OptionCount,
            StoreId = p.StoreId
        }).ToList();

        return result;
    }
}
