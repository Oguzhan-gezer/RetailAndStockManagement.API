using RetailAndStockManagement.Data.EF;

public class CreateCartItemModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public CartModel Cart { get; set; }
}