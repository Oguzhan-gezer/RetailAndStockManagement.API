using System.ComponentModel.DataAnnotations.Schema;
namespace RetailAndStockManagement.Data.EF;

[Table("Sale", Schema = "dbo")]
public class SaleModel
{
    public int Id { get; set; }
    public string Barcode { get; set; }
    public int StoreId { get; set; }
    public int Quantity { get; set; }
    public DateTime SaleDate { get; set; }
    public int CustomerId { get; set; }



    [NotMapped]
    public string StoreLocation { get; set; }

}