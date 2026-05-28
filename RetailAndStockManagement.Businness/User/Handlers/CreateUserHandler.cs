using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.User.Models;
using RetailAndStockManagement.Businness.User.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.User.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, UserListModel>
{
    private readonly RetailAndStockManagementContext _context;

    public CreateUserHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<UserListModel> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var exists = await _context.Users.AnyAsync(u => u.Username == request.Username, cancellationToken);
        if (exists) throw new Exception("Kullanıcı adı zaten kullanımda.");

        var user = new UserModel
        {
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role,
            StoreId = request.StoreId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        var storeName = "";
        if (user.StoreId.HasValue)
        {
            var store = await _context.Stores.FindAsync(user.StoreId);
            storeName = store?.StoreLocation;
        }

        return new UserListModel
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            StoreId = user.StoreId,
            StoreName = storeName,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}
