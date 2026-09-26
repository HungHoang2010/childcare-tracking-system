using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly string _connectionString;
    public Repository(IConfiguration configuration) 
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
    }
    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
    public async Task<T?> GetTAsync(string query, object parameters)
    {
            using var connection = GetConnection();
            await connection.OpenAsync();
            return await connection.QuerySingleOrDefaultAsync<T>(query, parameters);

    }

    public async Task<List<T>> GetAllAsync(
    string query,
    object parameters)
{
        using var connection = GetConnection();
        await connection.OpenAsync();

    var result = await connection.QueryAsync<T>(
        query,
        parameters);
    return new List<T>(result);

}

    public async Task<int> ExecuteAsync(string query, object parameters)
    {
            using var connection = GetConnection();
            await connection.OpenAsync();
            return await connection.ExecuteAsync(query, parameters);
    }
  
}
    