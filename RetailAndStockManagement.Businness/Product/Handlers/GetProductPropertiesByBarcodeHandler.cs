using MediatR;
using RetailAndStockManagement.Businness.Product.Requests;
using RetailAndStockManagement.Data.EF;

public class GetProductPropertiesByBarcodeHandler : IRequestHandler<GetProductPropertiesByBarcodeRequest, GetProductPropertiesByBarcodeModel>
{
    private readonly RetailAndStockManagementContext _context;
    public GetProductPropertiesByBarcodeHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<GetProductPropertiesByBarcodeModel> Handle(GetProductPropertiesByBarcodeRequest request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Barcode);

        if (product == null)
            return null;

        return new GetProductPropertiesByBarcodeModel
        {
            Barcode = product.Barcode,
            ProductCode = product.ProductCode,
            Price = product.Price,
            ProductProperties = product.ProductProperties,
            ImageUrl = string.IsNullOrEmpty(product.ImageBase64)
                ? null
                : $"data:image/jpeg;base64,{product.ImageBase64}"
        };
    }
}
