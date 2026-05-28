using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.User.Models;
using RetailAndStockManagement.Businness.User.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.User.Handlers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, List<UserListModel>>
{
    private readonly RetailAndStockManagementContext _context;

    public GetAllUsersHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<List<UserListModel>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Store)
            .OrderByDescending(u => u.CreatedAt)
            .Select(u => new UserListModel
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role,
                StoreId = u.StoreId,
                StoreName = u.Store != null ? u.Store.StoreLocation : null,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            }).ToListAsync(cancellationToken);
    }
}
