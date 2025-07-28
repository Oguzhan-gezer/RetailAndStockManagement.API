using MediatR;

public class AddToCartRequest : IRequest<AddToCartModel>
{
    public int CustomerId { get; set; }
    public string Barcode { get; set; }
    public int StoreId { get; set; }
    public int Quantity { get; set; }
}