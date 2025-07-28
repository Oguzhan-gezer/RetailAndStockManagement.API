using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Data.EF;

[Table("Product", Schema = "dbo")]

public class ProductModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Barcode { get; set; }

    [Required]
    public string ProductCode { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public string ProductProperties { get; set; }

    public string? ImageBase64 { get; set; }
}
