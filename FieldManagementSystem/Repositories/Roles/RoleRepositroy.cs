using FieldManagementSystemAPI.Entites;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FieldManagementSystemAPI.Repositories.Roles
{
    public class RoleRepositroy : IRoleRepository
    {
        private readonly string _connectionString;
        public RoleRepositroy(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task<IEnumerable<Role>> GetAll()
        {
            List<Role> roles = new List<Role>();
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("GetRoleById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                roles.Add(new Role() { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            }
            return roles;
        }

        public async Task<Role?> GetById(int id)
        {
            Role role = null;
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("GetRoleById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoleId", id);
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                role = new Role() { Id = reader.GetInt32(0), Name = reader.GetString(1) };
            }
            return role;
        }
    }
}
