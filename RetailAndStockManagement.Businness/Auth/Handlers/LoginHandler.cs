using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Auth.Models;
using RetailAndStockManagement.Businness.Auth.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.Auth.Handlers;

public class LoginHandler : IRequestHandler<LoginRequest, LoginModel>
{
    private readonly RetailAndStockManagementContext _context;

    public LoginHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<LoginModel> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Store)
            .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (user == null || !user.IsActive)
        {
            return new LoginModel { IsSuccess = false, Message = "Kullanıcı adı veya şifre hatalı." };
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            return new LoginModel { IsSuccess = false, Message = "Kullanıcı adı veya şifre hatalı." };
        }

        var token = JwtTokenHelper.GenerateToken(
            userId: user.Id,
            username: user.Username,
            role: user.Role,
            fullName: user.FullName,
            storeId: user.StoreId,
            storeName: user.Store?.StoreLocation
        );

        return new LoginModel
        {
            IsSuccess = true,
            Message = "Giriş başarılı.",
            Token = token,
            UserId = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            Role = user.Role,
            StoreId = user.StoreId,
            StoreName = user.Store?.StoreLocation
        };
    }
}
