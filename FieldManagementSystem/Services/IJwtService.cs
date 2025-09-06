using FieldManagementSystemAPI.Entites;

namespace FieldManagementSystemAPI.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
