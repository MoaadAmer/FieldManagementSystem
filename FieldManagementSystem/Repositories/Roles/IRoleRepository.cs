using FieldManagementSystemAPI.Entites;
using System.Collections.Generic;

namespace FieldManagementSystemAPI.Repositories.Roles
{
    public interface IRoleRepository
    {
        Task<Role?> GetById(int id);
        Task<IEnumerable<Role>> GetAll();
    }
}
