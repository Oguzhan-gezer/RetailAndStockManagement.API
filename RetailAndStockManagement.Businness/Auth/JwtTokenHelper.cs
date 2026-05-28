using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RetailAndStockManagement.Businness.Auth;

public static class JwtTokenHelper
{
    public static string GenerateToken(int userId, string username, string role, string fullName, int? storeId, string? storeName)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RetailAndStockManagement_SuperSecretKey_2024_MiniSAP!@#$%"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role),
            new("fullName", fullName)
        };

        if (storeId.HasValue)
            claims.Add(new Claim("storeId", storeId.Value.ToString()));
        if (!string.IsNullOrEmpty(storeName))
            claims.Add(new Claim("storeName", storeName));

        var token = new JwtSecurityToken(
            issuer: "RetailAndStockManagement",
            audience: "RetailAndStockManagement",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
