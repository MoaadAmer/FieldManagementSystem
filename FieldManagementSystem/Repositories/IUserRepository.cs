using FieldManagementSystemAPI.Entites;

namespace FieldManagementSystemAPI.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task<IEnumerable<User>> GetAll();
        Task Update(int id, User user);
        Task Delete(int id);
    }
}
