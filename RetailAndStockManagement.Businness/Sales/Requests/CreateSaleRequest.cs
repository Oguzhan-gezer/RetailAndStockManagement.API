using MediatR;
using RetailAndStockManagement.Businness.Sales.Models;

namespace RetailAndStockManagement.Businness.Sales.Requests
{
    public class CreateSaleRequest : IRequest<CreateSaleModel>
    {
        public string Barcode { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
    }
}