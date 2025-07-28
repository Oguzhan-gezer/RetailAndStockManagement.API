using RetailAndStockManagement.Data.EF;
using System.ComponentModel.DataAnnotations;

public class CountryModel
{
    [Key]
    public int CountryId { get; set; }
    public string CountryName { get; set; }

    public ICollection<RegionModel> Regions { get; set; }
}