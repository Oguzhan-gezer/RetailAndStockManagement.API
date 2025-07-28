using MediatR;
using Microsoft.AspNetCore.Http;
using RetailAndStockManagement.Businness.Product.Models;

namespace RetailAndStockManagement.Businness.Product.Requests;

public class UploadImageRequest : IRequest<UploadImageModel>
{
    public string Barcode { get; set; }
    public IFormFile Image { get; set; }
}
