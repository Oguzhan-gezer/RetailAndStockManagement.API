using MediatR;
using RetailAndStockManagement.Businness.Stock.Models;
using System.Collections.Generic;

public class GetStoreStockByBarcodeRequest : IRequest<List<StoreStockModel>>
{
    public string Barcode { get; set; }
}