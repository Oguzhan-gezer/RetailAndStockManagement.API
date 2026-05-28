using MediatR;
using RetailAndStockManagement.Businness.TransferRequest.Models;

namespace RetailAndStockManagement.Businness.TransferRequest.Requests;

public class CreateTransferRequestRequest : IRequest<TransferRequestListModel>
{
    public int SourceStoreId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public int ReqXS { get; set; }
    public int ReqS { get; set; }
    public int ReqM { get; set; }
    public int ReqL { get; set; }
    public int ReqXL { get; set; }
    public int ReqXXL { get; set; }
    public int ReqXXXL { get; set; }
    public string? Description { get; set; }
    public int CreatedByUserId { get; set; }
}

public class GetMyTransferRequestsRequest : IRequest<List<TransferRequestListModel>>
{
    public int StoreId { get; set; }
}

public class CancelTransferRequestRequest : IRequest<bool>
{
    public int RequestId { get; set; }
    public int UserId { get; set; }
}

public class SearchTransferRequestsByBarcodeRequest : IRequest<List<TransferRequestListModel>>
{
    public string Barcode { get; set; } = string.Empty;
    public int? ExcludeStoreId { get; set; }
}

public class GetAllActiveTransferRequestsRequest : IRequest<List<TransferRequestListModel>>
{
    public int? ExcludeStoreId { get; set; }
}
