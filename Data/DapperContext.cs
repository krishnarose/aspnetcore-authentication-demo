using System.Data;
using Npgsql;

namespace AuthProject.Data
{
    public class DapperContext
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public IDbConnection CreateConnection()
        {
            try
            {
                var connection = new NpgsqlConnection(_connectionString);
                Console.WriteLine("Database connection established.");
                return connection;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error establishing database connection: {ex.Message}");
                throw;
            }
        }
    }

}