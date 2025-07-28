using MediatR;
using RetailAndStockManagement.Businness.Product.Models;
using RetailAndStockManagement.Businness.Product.Requests;
using RetailAndStockManagement.Data.EF;

public class UploadImageHandler : IRequestHandler<UploadImageRequest, UploadImageModel>
{
    private readonly RetailAndStockManagementContext _context;

    public UploadImageHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<UploadImageModel> Handle(UploadImageRequest request, CancellationToken cancellationToken)
    {
        // Barkoda göre ürünü getir
        var product = await _context.Products.FindAsync(request.Barcode);
        if (product == null)
        {
            return new UploadImageModel { Message = "Ürün bulunamadı." };
        }

        // Görsel var mı?
        if (request.Image == null || request.Image.Length == 0)
        {
            return new UploadImageModel { Message = "Görsel bulunamadı." };
        }

        // Görseli base64 string'e çevir
        using var ms = new MemoryStream();
        await request.Image.CopyToAsync(ms, cancellationToken);
        var base64 = Convert.ToBase64String(ms.ToArray());

        // Kolona yaz (örneğin ProductProperties)
        product.ImageBase64 = base64;

        // Değişikliği zorla
        _context.Entry(product).Property(x => x.ImageBase64).IsModified = true;

        // DB'ye kaydet
        await _context.SaveChangesAsync(cancellationToken);

        return new UploadImageModel { Message = "Başarıyla güncellendi." };
    }
}
