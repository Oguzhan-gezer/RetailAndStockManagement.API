using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.TransferRequest.Models;
using RetailAndStockManagement.Businness.TransferRequest.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.TransferRequest.Handlers;

public class CreateTransferRequestHandler : IRequestHandler<CreateTransferRequestRequest, TransferRequestListModel>
{
    private readonly RetailAndStockManagementContext _context;
    public CreateTransferRequestHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<TransferRequestListModel> Handle(CreateTransferRequestRequest request, CancellationToken cancellationToken)
    {
        var productStore = await _context.ProductStores
            .Include(ps => ps.Product)
            .Include(ps => ps.Store)
            .FirstOrDefaultAsync(ps => ps.Barcode == request.Barcode && ps.StoreId == request.SourceStoreId, cancellationToken);

        int totalReq = request.ReqXS + request.ReqS + request.ReqM + request.ReqL + request.ReqXL + request.ReqXXL + request.ReqXXXL;
        if (totalReq <= 0) throw new Exception("En az bir beden için miktar girmelisiniz.");

        if (productStore == null) throw new Exception("Ürün bu mağazada bulunamadı.");
        
        if (productStore.SizeXS < request.ReqXS || productStore.SizeS < request.ReqS || productStore.SizeM < request.ReqM || 
            productStore.SizeL < request.ReqL || productStore.SizeXL < request.ReqXL || productStore.SizeXXL < request.ReqXXL || productStore.SizeXXXL < request.ReqXXXL) 
        {
            throw new Exception("Bazı bedenler için yeterli stok yok.");
        }

        var activeRequestExists = await _context.TransferRequests
            .AnyAsync(tr => tr.Barcode == request.Barcode && tr.SourceStoreId == request.SourceStoreId && tr.Status == "Active", cancellationToken);

        if (activeRequestExists) throw new Exception("Bu ürün için mağazanızda zaten aktif bir talep var.");

        var user = await _context.Users.FindAsync(new object[] { request.CreatedByUserId }, cancellationToken);

        var transferRequest = new TransferRequestModel
        {
            Barcode = request.Barcode,
            SourceStoreId = request.SourceStoreId,
            AvailableQuantity = totalReq,
            RemainingQuantity = totalReq,
            ReqXS = request.ReqXS, ReqS = request.ReqS, ReqM = request.ReqM, ReqL = request.ReqL, ReqXL = request.ReqXL, ReqXXL = request.ReqXXL, ReqXXXL = request.ReqXXXL,
            RemXS = request.ReqXS, RemS = request.ReqS, RemM = request.ReqM, RemL = request.ReqL, RemXL = request.ReqXL, RemXXL = request.ReqXXL, RemXXXL = request.ReqXXXL,
            Status = "Active",
            Description = request.Description,
            CreatedByUserId = request.CreatedByUserId,
            CreatedAt = DateTime.UtcNow
        };

        productStore.SizeXS -= request.ReqXS;
        productStore.SizeS -= request.ReqS;
        productStore.SizeM -= request.ReqM;
        productStore.SizeL -= request.ReqL;
        productStore.SizeXL -= request.ReqXL;
        productStore.SizeXXL -= request.ReqXXL;
        productStore.SizeXXXL -= request.ReqXXXL;
        productStore.OptionCount -= totalReq;

        _context.Entry(productStore).State = EntityState.Modified;
        
        _context.TransferRequests.Add(transferRequest);
        await _context.SaveChangesAsync(cancellationToken);

        return new TransferRequestListModel
        {
            Id = transferRequest.Id,
            Barcode = productStore.Barcode,
            ProductCode = productStore.Product.ProductCode,
            ProductImage = productStore.Product.ImageBase64,
            ProductPrice = productStore.Product.Price,
            ProductProperties = productStore.Product.ProductProperties,
            SourceStoreId = productStore.StoreId,
            SourceStoreName = productStore.Store.StoreLocation,
            AvailableQuantity = transferRequest.AvailableQuantity,
            RemainingQuantity = transferRequest.RemainingQuantity,
            ReqXS = transferRequest.ReqXS, ReqS = transferRequest.ReqS, ReqM = transferRequest.ReqM, ReqL = transferRequest.ReqL, ReqXL = transferRequest.ReqXL, ReqXXL = transferRequest.ReqXXL, ReqXXXL = transferRequest.ReqXXXL,
            RemXS = transferRequest.RemXS, RemS = transferRequest.RemS, RemM = transferRequest.RemM, RemL = transferRequest.RemL, RemXL = transferRequest.RemXL, RemXXL = transferRequest.RemXXL, RemXXXL = transferRequest.RemXXXL,
            Status = transferRequest.Status,
            Description = transferRequest.Description,
            CreatedByUserName = user?.FullName ?? "",
            CreatedAt = transferRequest.CreatedAt,
            StoreStock = productStore.OptionCount
        };
    }
}

public class GetMyTransferRequestsHandler : IRequestHandler<GetMyTransferRequestsRequest, List<TransferRequestListModel>>
{
    private readonly RetailAndStockManagementContext _context;
    public GetMyTransferRequestsHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<List<TransferRequestListModel>> Handle(GetMyTransferRequestsRequest request, CancellationToken cancellationToken)
    {
        return await _context.TransferRequests
            .Include(tr => tr.Product)
            .Include(tr => tr.SourceStore)
            .Include(tr => tr.CreatedByUser)
            .Where(tr => tr.SourceStoreId == request.StoreId)
            .OrderByDescending(tr => tr.CreatedAt)
            .Select(tr => new TransferRequestListModel
            {
                Id = tr.Id,
                Barcode = tr.Barcode,
                ProductCode = tr.Product.ProductCode,
                ProductImage = tr.Product.ImageBase64,
                ProductPrice = tr.Product.Price,
                ProductProperties = tr.Product.ProductProperties,
                SourceStoreId = tr.SourceStoreId,
                SourceStoreName = tr.SourceStore.StoreLocation,
                AvailableQuantity = tr.AvailableQuantity,
                RemainingQuantity = tr.RemainingQuantity,
                ReqXS = tr.ReqXS, ReqS = tr.ReqS, ReqM = tr.ReqM, ReqL = tr.ReqL, ReqXL = tr.ReqXL, ReqXXL = tr.ReqXXL, ReqXXXL = tr.ReqXXXL,
                RemXS = tr.RemXS, RemS = tr.RemS, RemM = tr.RemM, RemL = tr.RemL, RemXL = tr.RemXL, RemXXL = tr.RemXXL, RemXXXL = tr.RemXXXL,
                Status = tr.Status,
                Description = tr.Description,
                CreatedByUserName = tr.CreatedByUser.FullName,
                CreatedAt = tr.CreatedAt,
                StoreStock = _context.ProductStores.Where(ps => ps.Barcode == tr.Barcode && ps.StoreId == tr.SourceStoreId).Select(ps => ps.OptionCount).FirstOrDefault()
            }).ToListAsync(cancellationToken);
    }
}

public class CancelTransferRequestHandler : IRequestHandler<CancelTransferRequestRequest, bool>
{
    private readonly RetailAndStockManagementContext _context;
    public CancelTransferRequestHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<bool> Handle(CancelTransferRequestRequest request, CancellationToken cancellationToken)
    {
        var tr = await _context.TransferRequests.FindAsync(new object[] { request.RequestId }, cancellationToken);
        if (tr == null) return false;
        if (tr.CreatedByUserId != request.UserId) throw new Exception("Sadece kendi taleplerinizi iptal edebilirsiniz.");
        if (tr.Status != "Active") throw new Exception("Sadece aktif talepler iptal edilebilir.");

        tr.Status = "Cancelled";
        tr.UpdatedAt = DateTime.UtcNow;
        _context.Entry(tr).State = EntityState.Modified;

        var productStore = await _context.ProductStores.FirstOrDefaultAsync(ps => ps.Barcode == tr.Barcode && ps.StoreId == tr.SourceStoreId, cancellationToken);
        if (productStore != null)
        {
            productStore.SizeXS += tr.RemXS;
            productStore.SizeS += tr.RemS;
            productStore.SizeM += tr.RemM;
            productStore.SizeL += tr.RemL;
            productStore.SizeXL += tr.RemXL;
            productStore.SizeXXL += tr.RemXXL;
            productStore.SizeXXXL += tr.RemXXXL;
            productStore.OptionCount += tr.RemainingQuantity;
            _context.Entry(productStore).State = EntityState.Modified;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public class SearchTransferRequestsByBarcodeHandler : IRequestHandler<SearchTransferRequestsByBarcodeRequest, List<TransferRequestListModel>>
{
    private readonly RetailAndStockManagementContext _context;
    public SearchTransferRequestsByBarcodeHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<List<TransferRequestListModel>> Handle(SearchTransferRequestsByBarcodeRequest request, CancellationToken cancellationToken)
    {
        var query = _context.TransferRequests
            .Include(tr => tr.Product)
            .Include(tr => tr.SourceStore)
            .Include(tr => tr.CreatedByUser)
            .Where(tr => (tr.Barcode.Contains(request.Barcode) || tr.Product.ProductProperties.Contains(request.Barcode)) && tr.Status == "Active");

        if (request.ExcludeStoreId.HasValue)
            query = query.Where(tr => tr.SourceStoreId != request.ExcludeStoreId.Value);

        return await query
            .OrderByDescending(tr => tr.CreatedAt)
            .Select(tr => new TransferRequestListModel
            {
                Id = tr.Id,
                Barcode = tr.Barcode,
                ProductCode = tr.Product.ProductCode,
                ProductImage = tr.Product.ImageBase64,
                ProductPrice = tr.Product.Price,
                ProductProperties = tr.Product.ProductProperties,
                SourceStoreId = tr.SourceStoreId,
                SourceStoreName = tr.SourceStore.StoreLocation,
                AvailableQuantity = tr.AvailableQuantity,
                RemainingQuantity = tr.RemainingQuantity,
                ReqXS = tr.ReqXS, ReqS = tr.ReqS, ReqM = tr.ReqM, ReqL = tr.ReqL, ReqXL = tr.ReqXL, ReqXXL = tr.ReqXXL, ReqXXXL = tr.ReqXXXL,
                RemXS = tr.RemXS, RemS = tr.RemS, RemM = tr.RemM, RemL = tr.RemL, RemXL = tr.RemXL, RemXXL = tr.RemXXL, RemXXXL = tr.RemXXXL,
                Status = tr.Status,
                Description = tr.Description,
                CreatedByUserName = tr.CreatedByUser.FullName,
                CreatedAt = tr.CreatedAt,
                StoreStock = _context.ProductStores.Where(ps => ps.Barcode == tr.Barcode && ps.StoreId == tr.SourceStoreId).Select(ps => ps.OptionCount).FirstOrDefault()
            }).ToListAsync(cancellationToken);
    }
}

public class GetAllActiveTransferRequestsHandler : IRequestHandler<GetAllActiveTransferRequestsRequest, List<TransferRequestListModel>>
{
    private readonly RetailAndStockManagementContext _context;
    public GetAllActiveTransferRequestsHandler(RetailAndStockManagementContext context) => _context = context;

    public async Task<List<TransferRequestListModel>> Handle(GetAllActiveTransferRequestsRequest request, CancellationToken cancellationToken)
    {
        var query = _context.TransferRequests
            .Include(tr => tr.Product)
            .Include(tr => tr.SourceStore)
            .Include(tr => tr.CreatedByUser)
            .Where(tr => tr.Status == "Active");

        if (request.ExcludeStoreId.HasValue)
            query = query.Where(tr => tr.SourceStoreId != request.ExcludeStoreId.Value);

        return await query
            .OrderByDescending(tr => tr.CreatedAt)
            .Select(tr => new TransferRequestListModel
            {
                Id = tr.Id,
                Barcode = tr.Barcode,
                ProductCode = tr.Product.ProductCode,
                ProductImage = tr.Product.ImageBase64,
                ProductPrice = tr.Product.Price,
                ProductProperties = tr.Product.ProductProperties,
                SourceStoreId = tr.SourceStoreId,
                SourceStoreName = tr.SourceStore.StoreLocation,
                AvailableQuantity = tr.AvailableQuantity,
                RemainingQuantity = tr.RemainingQuantity,
                ReqXS = tr.ReqXS, ReqS = tr.ReqS, ReqM = tr.ReqM, ReqL = tr.ReqL, ReqXL = tr.ReqXL, ReqXXL = tr.ReqXXL, ReqXXXL = tr.ReqXXXL,
                RemXS = tr.RemXS, RemS = tr.RemS, RemM = tr.RemM, RemL = tr.RemL, RemXL = tr.RemXL, RemXXL = tr.RemXXL, RemXXXL = tr.RemXXXL,
                Status = tr.Status,
                Description = tr.Description,
                CreatedByUserName = tr.CreatedByUser.FullName,
                CreatedAt = tr.CreatedAt,
                StoreStock = _context.ProductStores.Where(ps => ps.Barcode == tr.Barcode && ps.StoreId == tr.SourceStoreId).Select(ps => ps.OptionCount).FirstOrDefault()
            }).ToListAsync(cancellationToken);
    }
}
