using MediatR;
using RetailAndStockManagement.Businness.Auth.Models;

namespace RetailAndStockManagement.Businness.Auth.Requests;

public class LoginRequest : IRequest<LoginModel>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
