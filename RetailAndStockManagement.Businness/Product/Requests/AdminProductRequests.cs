using MediatR;

namespace RetailAndStockManagement.Businness.Product.Requests;

public class CreateProductRequest : IRequest<bool>
{
    public string Barcode { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ProductProperties { get; set; } = string.Empty;
    public string? ImageBase64 { get; set; }
}

public class UpdateProductRequest : IRequest<bool>
{
    public string Barcode { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ProductProperties { get; set; } = string.Empty;
    public string? ImageBase64 { get; set; }
}

public class DeleteProductRequest : IRequest<bool>
{
    public string Barcode { get; set; } = string.Empty;
}

public class AssignProductToStoreRequest : IRequest<bool>
{
    public string Barcode { get; set; } = string.Empty;
    public int StoreId { get; set; }
    public int SizeXS { get; set; }
    public int SizeS { get; set; }
    public int SizeM { get; set; }
    public int SizeL { get; set; }
    public int SizeXL { get; set; }
    public int SizeXXL { get; set; }
    public int SizeXXXL { get; set; }
    public int OptionCount { get; set; }
}
