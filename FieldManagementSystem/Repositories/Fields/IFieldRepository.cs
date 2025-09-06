using FieldManagementSystemAPI.Entites;

namespace FieldManagementSystemAPI.Repositories.Fields
{
    public interface IFieldRepository
    {
        Task Add(Field field);
        Task<Field?> GetById(int id);
        Task<IEnumerable<Field>> GetByUserId(int userId);
        Task Update(int id,Field field);
        Task Delete(int id);
    }
}
