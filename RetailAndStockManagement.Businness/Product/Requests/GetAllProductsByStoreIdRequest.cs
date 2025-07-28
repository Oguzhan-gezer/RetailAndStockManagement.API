using MediatR;
using RetailAndStockManagement.Businness.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Product.Requests;

public class GetAllProductsByStoreIdRequest : IRequest<List<GetAllProductsByStoreIdModel>>
{
    public int StoreId { get; set; }
}
