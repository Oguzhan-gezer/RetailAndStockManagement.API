using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.User.Models;
using RetailAndStockManagement.Businness.User.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.User.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UserListModel>
{
    private readonly RetailAndStockManagementContext _context;

    public UpdateUserHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<UserListModel> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
        if (user == null) throw new Exception("Kullanıcı bulunamadı.");

        if (user.Username != request.Username)
        {
            var exists = await _context.Users.AnyAsync(u => u.Username == request.Username, cancellationToken);
            if (exists) throw new Exception("Kullanıcı adı zaten kullanımda.");
            user.Username = request.Username;
        }

        user.FullName = request.FullName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Role = request.Role;
        user.StoreId = request.StoreId;

        if (!string.IsNullOrEmpty(request.Password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        }

        _context.Entry(user).State = EntityState.Modified;
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
