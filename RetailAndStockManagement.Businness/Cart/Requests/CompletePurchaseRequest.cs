using MediatR;
using RetailAndStockManagement.Businness.Cart.Models;

namespace RetailAndStockManagement.Businness.Cart.Requests
{
    public class CompletePurchaseRequest : IRequest<CompletePurchaseModel>
    {
        public int CustomerId { get; set; }
    }
}