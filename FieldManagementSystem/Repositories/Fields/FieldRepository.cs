using FieldManagementSystemAPI.Entites;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FieldManagementSystemAPI.Repositories.Fields
{
    public class FieldRepository : IFieldRepository
    {
        private readonly string _connectionString = "";
        public FieldRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public async Task Add(Field field)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("AddField", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", field.Name);
            cmd.Parameters.AddWithValue("@Location", field.Location);
            cmd.Parameters.AddWithValue("@UserId", field.UserId);

            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                field.Id = reader.GetInt32(0);
            }
        }

        public async Task<Field?> GetById(int id)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("GetFieldById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FieldId", id);

            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Field()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Location = reader.GetString(2),
                    UserId = reader.GetInt32(3)
                };
            }
            return null;
        }

        public async Task<IEnumerable<Field>> GetByUserId(int userId)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("GetFieldsByUserId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", userId);

            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            List<Field> fields = new List<Field>();
            while (await reader.ReadAsync())
            {
                fields.Add(new Field()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Location = reader.GetString(2),
                    UserId = reader.GetInt32(3)
                });
            }
            return fields;
        }

        public async Task Update(int id, Field field)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("UpdateField", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FieldId", id);
            cmd.Parameters.AddWithValue("@Name", field.Name);
            cmd.Parameters.AddWithValue("@Location", field.Location);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
        public async Task Delete(int id)
        {
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("DeleteField", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FieldId", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
