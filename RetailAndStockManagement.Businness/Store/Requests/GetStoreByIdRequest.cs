using MediatR;
using RetailAndStockManagement.Business.Store.Models;

namespace RetailAndStockManagement.Business.Store.Requests
{
    public class GetStoreLocationByIdRequest : IRequest<GetStoreLocationByIdModel>
    {
        public int StoreId { get; set; }

        public GetStoreLocationByIdRequest(int storeId)
        {
            StoreId = storeId;
        }
    }
}