namespace RetailAndStockManagement.Business.Sales.Models
{
    public class AlsoBoughtProductModel
    {
        public string Barcode { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public string ImageBase64 { get; set; }
        public int OptionCount { get; set; }
        public int StoreId { get; set; }
    }
}