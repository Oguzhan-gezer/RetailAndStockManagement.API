namespace RetailAndStockManagement.Businness.TransferOrder.Models;

public class TransferOrderListModel
{
    public int Id { get; set; }
    public int TransferRequestId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;
    public string? ProductImage { get; set; }
    public decimal ProductPrice { get; set; }
    public int SourceStoreId { get; set; }
    public string SourceStoreName { get; set; } = string.Empty;
    public int TargetStoreId { get; set; }
    public string TargetStoreName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Status { get; set; } = string.Empty;
    public string CreatedByUserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Note { get; set; }
    
    public int QtyXS { get; set; }
    public int QtyS { get; set; }
    public int QtyM { get; set; }
    public int QtyL { get; set; }
    public int QtyXL { get; set; }
    public int QtyXXL { get; set; }
    public int QtyXXXL { get; set; }
}
