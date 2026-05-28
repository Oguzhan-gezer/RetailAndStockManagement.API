using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Data.EF;

[Table("ProductStore", Schema = "dbo")]
public class ProductStoreModel
{
    [Key]
    public int Id { get; set; }

    public string Barcode { get; set; }
    public int StoreId { get; set; }
    public int OptionCount { get; set; }
    public int SizeXS { get; set; }
    public int SizeS { get; set; }
    public int SizeM { get; set; }
    public int SizeL { get; set; }
    public int SizeXL { get; set; }
    public int SizeXXL { get; set; }
    public int SizeXXXL { get; set; }

    [ForeignKey("Barcode")]
    public ProductModel Product { get; set; }

    [ForeignKey("StoreId")]
    public StoreModel Store { get; set; }

    [NotMapped]
    public string StoreLocation { get; set; }
}