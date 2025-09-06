using FieldManagementSystemAPI.Entites;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FieldManagementSystemAPI.Repositories.Contorllers
{
    public class ControllerRepository : IControllerRepository
    {
        private string? _connectionString;

        public ControllerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task Add(Controller controller)
        {
            using SqlConnection conn = new(_connectionString);

            using SqlCommand cmd = new("AddController", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", controller.Type);
            cmd.Parameters.AddWithValue("@FieldId", controller.FieldId);

            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                controller.Id = reader.GetInt32(0);
            }
        }
        public async Task<Controller?> GetById(int id)
        {
            using SqlConnection conn = new(_connectionString);

            using SqlCommand cmd = new("GetControllerById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ControllerId", id);

            await conn.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Controller()
                {
                    Id = reader.GetInt32(0),
                    Type = reader.GetString(1),
                    FieldId = reader.GetInt32(2)
                };
            }
            return null;
        }
        public async Task<IEnumerable<Controller>> GetByFieldId(int fieldId)
        {
            using SqlConnection conn = new(_connectionString);

            using SqlCommand cmd = new("GetControllersByFieldId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FieldId", fieldId);

            await conn.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            var controllers = new List<Controller>();
            while (await reader.ReadAsync())
            {
                controllers.Add(new Controller()
                {
                    Id = reader.GetInt32(0),
                    Type = reader.GetString(1),
                    FieldId = reader.GetInt32(2)
                });
            }
            return controllers;
        }

        public async Task Delete(int id)
        {
            SqlConnection conn = new(_connectionString);

            SqlCommand cmd = new("DeleteController", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ControllerId", id);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
