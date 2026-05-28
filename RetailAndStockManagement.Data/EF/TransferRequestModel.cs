using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailAndStockManagement.Data.EF;

[Table("TransferRequest", Schema = "dbo")]
public class TransferRequestModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Barcode { get; set; } = string.Empty;

    [ForeignKey("Barcode")]
    public ProductModel Product { get; set; } = null!;

    [Required]
    public int SourceStoreId { get; set; }

    [ForeignKey("SourceStoreId")]
    public StoreModel SourceStore { get; set; } = null!;

    [Required]
    public int AvailableQuantity { get; set; }

    public int RemainingQuantity { get; set; }

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

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Active"; // "Active", "Completed", "Cancelled"

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public int CreatedByUserId { get; set; }

    [ForeignKey("CreatedByUserId")]
    public UserModel CreatedByUser { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<TransferOrderModel> TransferOrders { get; set; } = new List<TransferOrderModel>();
}
