using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.TransferOrder.Models;
using RetailAndStockManagement.Businness.TransferOrder.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.TransferOrder.Handlers;

public class CreateTransferOrderHandler : IRequestHandler<CreateTransferOrderRequest, TransferOrderListModel>
{
    private readonly RetailAndStockManagementContext _context;
    public CreateTransferOrderHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<TransferOrderListModel> Handle(CreateTransferOrderRequest request, CancellationToken cancellationToken)
    {
        var tr = await _context.TransferRequests
            .Include(t => t.Product)
            .Include(t => t.SourceStore)
            .FirstOrDefaultAsync(t => t.Id == request.TransferRequestId, cancellationToken);

        if (tr == null) throw new Exception("Talep bulunamadı.");
        if (tr.Status != "Active") throw new Exception("Bu talep artık aktif değil.");
        
        int totalQty = request.QtyXS + request.QtyS + request.QtyM + request.QtyL + request.QtyXL + request.QtyXXL + request.QtyXXXL;
        if (totalQty <= 0) throw new Exception("En az bir beden için sipariş girmelisiniz.");

        if (tr.RemXS < request.QtyXS || tr.RemS < request.QtyS || tr.RemM < request.QtyM || 
            tr.RemL < request.QtyL || tr.RemXL < request.QtyXL || tr.RemXXL < request.QtyXXL || tr.RemXXXL < request.QtyXXXL)
        {
            throw new Exception("Talep edilen beden miktarı yetersiz.");
        }

        // Target store stock update
        var targetStock = await _context.ProductStores
            .FirstOrDefaultAsync(ps => ps.Barcode == tr.Barcode && ps.StoreId == request.TargetStoreId, cancellationToken);
        if (targetStock != null) {
            targetStock.SizeXS += request.QtyXS;
            targetStock.SizeS += request.QtyS;
            targetStock.SizeM += request.QtyM;
            targetStock.SizeL += request.QtyL;
            targetStock.SizeXL += request.QtyXL;
            targetStock.SizeXXL += request.QtyXXL;
            targetStock.SizeXXXL += request.QtyXXXL;
            targetStock.OptionCount += totalQty;
            _context.Entry(targetStock).State = EntityState.Modified;
        } else {
            var newStock = new ProductStoreModel {
                Barcode = tr.Barcode,
                StoreId = request.TargetStoreId,
                SizeXS = request.QtyXS, SizeS = request.QtyS, SizeM = request.QtyM, SizeL = request.QtyL, SizeXL = request.QtyXL, SizeXXL = request.QtyXXL, SizeXXXL = request.QtyXXXL,
                OptionCount = totalQty
            };
            _context.ProductStores.Add(newStock);
        }

        // Update transfer request
        tr.RemainingQuantity -= totalQty;
        tr.RemXS -= request.QtyXS;
        tr.RemS -= request.QtyS;
        tr.RemM -= request.QtyM;
        tr.RemL -= request.QtyL;
        tr.RemXL -= request.QtyXL;
        tr.RemXXL -= request.QtyXXL;
        tr.RemXXXL -= request.QtyXXXL;
        if (tr.RemainingQuantity <= 0) tr.Status = "Completed";
        tr.UpdatedAt = DateTime.UtcNow;
        _context.Entry(tr).State = EntityState.Modified;

        // Create transfer order
        var order = new TransferOrderModel {
            TransferRequestId = tr.Id,
            TargetStoreId = request.TargetStoreId,
            Quantity = totalQty,
            QtyXS = request.QtyXS, QtyS = request.QtyS, QtyM = request.QtyM, QtyL = request.QtyL, QtyXL = request.QtyXL, QtyXXL = request.QtyXXL, QtyXXXL = request.QtyXXXL,
            Status = "Completed", // Auto-approved
            CreatedByUserId = request.CreatedByUserId,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = DateTime.UtcNow,
            Note = request.Note
        };
        _context.TransferOrders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

        var targetStore = await _context.Stores.FindAsync(new object[] { request.TargetStoreId }, cancellationToken);
        var user = await _context.Users.FindAsync(new object[] { request.CreatedByUserId }, cancellationToken);

        return new TransferOrderListModel
        {
            Id = order.Id,
            TransferRequestId = tr.Id,
            Barcode = tr.Barcode,
            ProductCode = tr.Product.ProductCode,
            ProductImage = tr.Product.ImageBase64,
            ProductPrice = tr.Product.Price,
            SourceStoreId = tr.SourceStoreId,
            SourceStoreName = tr.SourceStore.StoreLocation,
            TargetStoreId = request.TargetStoreId,
            TargetStoreName = targetStore?.StoreLocation ?? "",
            Quantity = totalQty,
            QtyXS = order.QtyXS, QtyS = order.QtyS, QtyM = order.QtyM, QtyL = order.QtyL, QtyXL = order.QtyXL, QtyXXL = order.QtyXXL, QtyXXXL = order.QtyXXXL,
            Status = order.Status,
            CreatedByUserName = user?.FullName ?? "",
            CreatedAt = order.CreatedAt,
            CompletedAt = order.CompletedAt,
            Note = order.Note
        };
    }
}

public class GetMyIncomingOrdersHandler : IRequestHandler<GetMyIncomingOrdersRequest, List<TransferOrderListModel>>
{
    private readonly RetailAndStockManagementContext _context;
    public GetMyIncomingOrdersHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<List<TransferOrderListModel>> Handle(GetMyIncomingOrdersRequest request, CancellationToken cancellationToken)
    {
        return await _context.TransferOrders
            .Include(to => to.TransferRequest).ThenInclude(tr => tr.Product)
            .Include(to => to.TransferRequest).ThenInclude(tr => tr.SourceStore)
            .Include(to => to.TargetStore)
            .Include(to => to.CreatedByUser)
            .Where(to => to.TransferRequest.SourceStoreId == request.StoreId)
            .OrderByDescending(to => to.CreatedAt)
            .Select(to => new TransferOrderListModel
            {
                Id = to.Id,
                TransferRequestId = to.TransferRequestId,
                Barcode = to.TransferRequest.Barcode,
                ProductCode = to.TransferRequest.Product.ProductCode,
                ProductImage = to.TransferRequest.Product.ImageBase64,
                ProductPrice = to.TransferRequest.Product.Price,
                SourceStoreId = to.TransferRequest.SourceStoreId,
                SourceStoreName = to.TransferRequest.SourceStore.StoreLocation,
                TargetStoreId = to.TargetStoreId,
                TargetStoreName = to.TargetStore.StoreLocation,
                Quantity = to.Quantity,
                QtyXS = to.QtyXS, QtyS = to.QtyS, QtyM = to.QtyM, QtyL = to.QtyL, QtyXL = to.QtyXL, QtyXXL = to.QtyXXL, QtyXXXL = to.QtyXXXL,
                Status = to.Status,
                CreatedByUserName = to.CreatedByUser.FullName,
                CreatedAt = to.CreatedAt,
                CompletedAt = to.CompletedAt,
                Note = to.Note
            }).ToListAsync(cancellationToken);
    }
}

public class GetMyOutgoingOrdersHandler : IRequestHandler<GetMyOutgoingOrdersRequest, List<TransferOrderListModel>>
{
    private readonly RetailAndStockManagementContext _context;
    public GetMyOutgoingOrdersHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<List<TransferOrderListModel>> Handle(GetMyOutgoingOrdersRequest request, CancellationToken cancellationToken)
    {
        return await _context.TransferOrders
            .Include(to => to.TransferRequest).ThenInclude(tr => tr.Product)
            .Include(to => to.TransferRequest).ThenInclude(tr => tr.SourceStore)
            .Include(to => to.TargetStore)
            .Include(to => to.CreatedByUser)
            .Where(to => to.TargetStoreId == request.StoreId)
            .OrderByDescending(to => to.CreatedAt)
            .Select(to => new TransferOrderListModel
            {
                Id = to.Id,
                TransferRequestId = to.TransferRequestId,
                Barcode = to.TransferRequest.Barcode,
                ProductCode = to.TransferRequest.Product.ProductCode,
                ProductImage = to.TransferRequest.Product.ImageBase64,
                ProductPrice = to.TransferRequest.Product.Price,
                SourceStoreId = to.TransferRequest.SourceStoreId,
                SourceStoreName = to.TransferRequest.SourceStore.StoreLocation,
                TargetStoreId = to.TargetStoreId,
                TargetStoreName = to.TargetStore.StoreLocation,
                Quantity = to.Quantity,
                QtyXS = to.QtyXS, QtyS = to.QtyS, QtyM = to.QtyM, QtyL = to.QtyL, QtyXL = to.QtyXL, QtyXXL = to.QtyXXL, QtyXXXL = to.QtyXXXL,
                Status = to.Status,
                CreatedByUserName = to.CreatedByUser.FullName,
                CreatedAt = to.CreatedAt,
                CompletedAt = to.CompletedAt,
                Note = to.Note
            }).ToListAsync(cancellationToken);
    }
}
