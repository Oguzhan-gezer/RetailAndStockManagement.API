using RetailAndStockManagement.Data.EF;
using System.Collections.Generic;

public class GetCartByCustomerModel
{
    public List<CartItemModel> Items { get; set; }
    public decimal TotalPrice { get; set; }
    public int StoreId { get; set; }
}