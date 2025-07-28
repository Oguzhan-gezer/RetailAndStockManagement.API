using MediatR;
using RetailAndStockManagement.Business.Sales.Models;
using System.Collections.Generic;

namespace RetailAndStockManagement.Business.Sales.Requests
{
    public class GetAlsoBoughtProductsRequest : IRequest<List<AlsoBoughtProductModel>>
    {
        public string Barcode { get; set; }
        public int CustomerId { get; set; }

        public GetAlsoBoughtProductsRequest(string barcode)
        {
            Barcode = barcode;
        }
    }
}
