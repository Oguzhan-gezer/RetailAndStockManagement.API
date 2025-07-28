using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailAndStockManagement.Data.EF;

[Table("Store", Schema = "dbo")]
public class StoreModel
{
    [Key]
    public int StoreId { get; set; }

    [Required]
    [Column("StoreLocation")]
    public string StoreLocation { get; set; }
    public string StoreLevel { get; set; }

    public int RegionId { get; set; }
    public RegionModel Region { get; set; }
}