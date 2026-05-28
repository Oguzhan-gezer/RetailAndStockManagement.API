using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Product.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.Product.Handlers;

public class AdminProductHandlers : 
    IRequestHandler<CreateProductRequest, bool>,
    IRequestHandler<UpdateProductRequest, bool>,
    IRequestHandler<DeleteProductRequest, bool>,
    IRequestHandler<AssignProductToStoreRequest, bool>
{
    private readonly RetailAndStockManagementContext _context;

    public AdminProductHandlers(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var exists = await _context.Products.AnyAsync(p => p.Barcode == request.Barcode, cancellationToken);
        if (exists) throw new Exception("Bu barkoda sahip bir ürün zaten var.");

        var product = new ProductModel
        {
            Barcode = request.Barcode,
            ProductCode = request.ProductCode,
            Price = request.Price,
            ProductProperties = request.ProductProperties,
            ImageBase64 = request.ImageBase64
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(new object[] { request.Barcode }, cancellationToken);
        if (product == null) throw new Exception("Ürün bulunamadı.");

        product.ProductCode = request.ProductCode;
        product.Price = request.Price;
        product.ProductProperties = request.ProductProperties;
        if (request.ImageBase64 != null) product.ImageBase64 = request.ImageBase64;

        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(new object[] { request.Barcode }, cancellationToken);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> Handle(AssignProductToStoreRequest request, CancellationToken cancellationToken)
    {
        var stock = await _context.ProductStores
            .FirstOrDefaultAsync(ps => ps.Barcode == request.Barcode && ps.StoreId == request.StoreId, cancellationToken);

        if (stock != null)
        {
            stock.SizeXS += request.SizeXS;
            stock.SizeS += request.SizeS;
            stock.SizeM += request.SizeM;
            stock.SizeL += request.SizeL;
            stock.SizeXL += request.SizeXL;
            stock.SizeXXL += request.SizeXXL;
            stock.SizeXXXL += request.SizeXXXL;
            stock.OptionCount += request.SizeXS + request.SizeS + request.SizeM + request.SizeL + request.SizeXL + request.SizeXXL + request.SizeXXXL;
            _context.Entry(stock).State = EntityState.Modified;
        }
        else
        {
            var newStock = new ProductStoreModel
            {
                Barcode = request.Barcode,
                StoreId = request.StoreId,
                SizeXS = request.SizeXS,
                SizeS = request.SizeS,
                SizeM = request.SizeM,
                SizeL = request.SizeL,
                SizeXL = request.SizeXL,
                SizeXXL = request.SizeXXL,
                SizeXXXL = request.SizeXXXL,
                OptionCount = request.SizeXS + request.SizeS + request.SizeM + request.SizeL + request.SizeXL + request.SizeXXL + request.SizeXXXL
            };
            _context.ProductStores.Add(newStock);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
