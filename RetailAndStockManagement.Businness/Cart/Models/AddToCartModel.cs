using RetailAndStockManagement.Data.EF;

public class AddToCartModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public List<CartItemModel> CartItems { get; set; }
}