using MediatR;
using RetailAndStockManagement.Businness.Cart.Models;

namespace RetailAndStockManagement.Businness.Cart.Requests
{
    public class RemoveCartItemRequest : IRequest<RemoveCartItemModel>
    {
        public int CustomerId { get; set; }
        public string Barcode { get; set; }
        public int StoreId { get; set; }
    }
}