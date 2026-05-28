using MediatR;
using RetailAndStockManagement.Businness.TransferOrder.Models;

namespace RetailAndStockManagement.Businness.TransferOrder.Requests;

public class CreateTransferOrderRequest : IRequest<TransferOrderListModel>
{
    public int TransferRequestId { get; set; }
    public int TargetStoreId { get; set; }
    public int QtyXS { get; set; }
    public int QtyS { get; set; }
    public int QtyM { get; set; }
    public int QtyL { get; set; }
    public int QtyXL { get; set; }
    public int QtyXXL { get; set; }
    public int QtyXXXL { get; set; }
    public int CreatedByUserId { get; set; }
    public string? Note { get; set; }
}

public class GetMyIncomingOrdersRequest : IRequest<List<TransferOrderListModel>>
{
    public int StoreId { get; set; }
}

public class GetMyOutgoingOrdersRequest : IRequest<List<TransferOrderListModel>>
{
    public int StoreId { get; set; }
}
