using MediatR;
using RetailAndStockManagement.Businness.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Product.Requests;

public class GetProductPropertiesByBarcodeRequest : IRequest<GetProductPropertiesByBarcodeModel>
{ 
    public string Barcode { get; set; }
}
