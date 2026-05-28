namespace RetailAndStockManagement.Businness.TransferRequest.Models;

public class TransferRequestListModel
{
    public int Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public string? ProductImage { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductProperties { get; set; } = string.Empty;
    public int SourceStoreId { get; set; }
    public string SourceStoreName { get; set; } = string.Empty;
    public int AvailableQuantity { get; set; }
    public int RemainingQuantity { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int StoreStock { get; set; }
    
    public int ReqXS { get; set; }
    public int ReqS { get; set; }
    public int ReqM { get; set; }
    public int ReqL { get; set; }
    public int ReqXL { get; set; }
    public int ReqXXL { get; set; }
    public int ReqXXXL { get; set; }

    public int RemXS { get; set; }
    public int RemS { get; set; }
    public int RemM { get; set; }
    public int RemL { get; set; }
    public int RemXL { get; set; }
    public int RemXXL { get; set; }
    public int RemXXXL { get; set; }
}
