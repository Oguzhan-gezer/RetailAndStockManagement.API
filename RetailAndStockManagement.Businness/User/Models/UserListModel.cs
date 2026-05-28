namespace RetailAndStockManagement.Businness.User.Models;

public class UserListModel
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Role { get; set; } = string.Empty;
    public int? StoreId { get; set; }
    public string? StoreName { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
