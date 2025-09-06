using FieldManagementSystemAPI.Entites;

namespace FieldManagementSystemAPI.Services
{
    public interface IJwtService
    {
        Task<string> GenerateToken(User user);
    }
}
