using FieldManagementSystemAPI.Entites;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FieldManagementSystemAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;
        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Default");
        }
        public async Task Add(User user)
        {
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new("AddUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                user.Id = reader.GetInt32(0);
            }
        }
        public async Task<User?> GetById(int id)
        {
            User? user = null;
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new("GetUserById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", id);
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                user = new User()
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    RoleId = reader.GetInt32(2)
                };
            }
            return user;
        }
        public async Task<User?> GetByEmail(string email)
        {
            User? user = null;
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new("GetUserByEmail", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", email);
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                user = new User()
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    RoleId = reader.GetInt32(2)
                };
            }
            return user;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            List<User> users = [];
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new("GetUsers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                users.Add(new User()
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    RoleId = reader.GetInt32(2)
                });
            }
            return users;
        }
        public async Task Update(int id, User user)
        {
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new("UpdateUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", id);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
        public async Task Delete(int id)
        {
            using SqlConnection conn = new(connectionString);
            using SqlCommand cmd = new("DeleteUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", id);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

    }
}
