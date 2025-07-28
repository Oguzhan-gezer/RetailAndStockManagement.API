using MediatR;
using RetailAndStockManagement.Businness.Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Businness.Store.Requests;

public class GetAllStoresRequest : IRequest<List<GetAllStoresModel>>
{

}
