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
}
