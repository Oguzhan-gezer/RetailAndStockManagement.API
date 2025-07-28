public class GetProductPropertiesByBarcodeModel
{
    public string Barcode { get; set; }
    public string ProductCode { get; set; }
    public decimal Price { get; set; }
    public string ProductProperties { get; set; }
    public string ImageUrl { get; set; } // base64 img olarak dönecek
}
