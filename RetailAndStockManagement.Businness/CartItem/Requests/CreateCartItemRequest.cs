using MediatR;
namespace RetailAndStockManagement.Businness.Sales.Requests
{
    public class CreateCartItemRequest : IRequest<CreateCartItemModel>
    {
        public int CustomerId { get; set; }
        public string Barcode { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? ImageBase64 { get; set; }
    }
}