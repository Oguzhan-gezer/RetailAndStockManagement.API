using RetailAndStockManagement.Data.EF;
using System.ComponentModel.DataAnnotations;

public class RegionModel
{
    [Key]
    public int RegionId { get; set; }
    public string RegionName { get; set; }

    public int CountryId { get; set; }
    public CountryModel Country { get; set; }

    public ICollection<StoreModel> Stores { get; set; }
}
