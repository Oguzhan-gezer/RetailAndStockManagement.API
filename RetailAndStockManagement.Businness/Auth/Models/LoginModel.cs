namespace RetailAndStockManagement.Businness.Auth.Models;

public class LoginModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Token { get; set; }
    public int? UserId { get; set; }
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public string? Role { get; set; }
    public int? StoreId { get; set; }
    public string? StoreName { get; set; }
}
