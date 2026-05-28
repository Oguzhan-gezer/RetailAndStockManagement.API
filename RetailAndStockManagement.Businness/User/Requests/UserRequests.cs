using MediatR;
using RetailAndStockManagement.Businness.User.Models;

namespace RetailAndStockManagement.Businness.User.Requests;

public class GetAllUsersRequest : IRequest<List<UserListModel>>
{
}

public class CreateUserRequest : IRequest<UserListModel>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Role { get; set; } = "StoreManager";
    public int? StoreId { get; set; }
}

public class UpdateUserRequest : IRequest<UserListModel>
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Password { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Role { get; set; } = "StoreManager";
    public int? StoreId { get; set; }
}

public class DeleteUserRequest : IRequest<bool>
{
    public int Id { get; set; }
}
