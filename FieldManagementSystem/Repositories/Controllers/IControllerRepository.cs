using FieldManagementSystemAPI.Entites;

namespace FieldManagementSystemAPI.Repositories.Contorllers
{
    public interface IControllerRepository
    {
        Task Add(Controller controller);
        Task<Controller?> GetById(int id);
        Task<IEnumerable<Controller>> GetByFieldId(int id);
        Task Delete(int id);
    }
}
