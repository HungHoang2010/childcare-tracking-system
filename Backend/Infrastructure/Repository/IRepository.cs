using System;
namespace Infrastructure.Repository;

public interface IRepository<T> where T : class
{
    Task<T?> GetTAsync(string query, object parameters);
    Task<List<T>> GetAllAsync(string query, object parameters);
}
