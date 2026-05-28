using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Product.Models;

public class GetAllProductsByStoreIdModel
{
    public string Barcode { get; set; }
    public string? ImageBase64 { get; set; }
    public int OptionCount { get; set; }
    public string ProductCode { get; set; }
    public decimal Price { get; set; }
    public string ProductProperties { get; set; } = string.Empty;

    public int SizeXS { get; set; }
    public int SizeS { get; set; }
    public int SizeM { get; set; }
    public int SizeL { get; set; }
    public int SizeXL { get; set; }
    public int SizeXXL { get; set; }
    public int SizeXXXL { get; set; }
}
