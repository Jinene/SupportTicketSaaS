using SupportTicket.Application.DTOs;
using SupportTicket.Infrastructure.Data;
using SupportTicket.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly JwtTokenGenerator _jwt;

    public AuthService(AppDbContext db, JwtTokenGenerator jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    public async Task<string?> Login(LoginDto dto)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null)
            return null;

        var passwordHash = Hash(dto.Password);

        if (passwordHash != user.PasswordHash)
            return null;

        return _jwt.GenerateToken(user.Id, user.Role, user.TenantId);
    }

    private static string Hash(string input)
    {
        using var sha = SHA256.Create();
        return Convert.ToBase64String(
            sha.ComputeHash(Encoding.UTF8.GetBytes(input))
        );
    }
}

