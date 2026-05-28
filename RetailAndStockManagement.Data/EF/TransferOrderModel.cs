using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailAndStockManagement.Data.EF;

[Table("TransferOrder", Schema = "dbo")]
public class TransferOrderModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TransferRequestId { get; set; }

    [ForeignKey("TransferRequestId")]
    public TransferRequestModel TransferRequest { get; set; } = null!;

    [Required]
    public int TargetStoreId { get; set; }

    [ForeignKey("TargetStoreId")]
    public StoreModel TargetStore { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }
    
    public int QtyXS { get; set; }
    public int QtyS { get; set; }
    public int QtyM { get; set; }
    public int QtyL { get; set; }
    public int QtyXL { get; set; }
    public int QtyXXL { get; set; }
    public int QtyXXXL { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Completed"; // Auto-approved: always "Completed"

    [Required]
    public int CreatedByUserId { get; set; }

    [ForeignKey("CreatedByUserId")]
    public UserModel CreatedByUser { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    [MaxLength(500)]
    public string? Note { get; set; }
}
