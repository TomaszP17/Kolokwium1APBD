using System.Data;
using System.Data.SqlClient;

namespace KolokwiumApp.Services;

public interface IDbService
{
    
}

public class DbService(IConfiguration configuration) : IDbService
{
    private async Task<SqlConnection> GetConnection()
    {
        var connection = new SqlConnection(configuration.GetConnectionString("Default"));
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        return connection;
    }


    public async Task<GetBookDto?> GetBookById(int id)
    {
        return null;
    }

    public async Task<int> AddBookAsync(AddBookDto bookDto)
    {
        return 0;
    }
}