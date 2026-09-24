using System;
using Application.IService;
using Application.RequestDTO;
using Application.ResponseDTO;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
namespace Application.Service;

public class RoleService : IRoleService
{
    private readonly string _connectionString;
    public RoleService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
    }

    public Task<BaseResponseDTO<Role>> AddRole(AddRoleRequest request)
    {
        try
        {
            string query = "INSERT INTO roles (RoleID, RoleName, RoleDescription) VALUES (@RoleID, @RoleName, @RoleDescription)";
            using(var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                Guid newRoleId = Guid.NewGuid();
                using(var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoleID", newRoleId);
                    command.Parameters.AddWithValue("@RoleName", request.RoleName);
                    command.Parameters.AddWithValue("@RoleDescription", request.RoleDescription);
                    int rowsAffected = command.ExecuteNonQuery();
                    if(rowsAffected > 0)
                    {
                        return Task.FromResult(new BaseResponseDTO<Role>
                        {
                            Status = 201,
                            Message = "Role added successfully.",
                            Data = new Role
                            {
                                RoleId = newRoleId,
                                RoleName = request.RoleName,
                                RoleDescription = request.RoleDescription
                            }
                        });
                    }
                    else
                    {
                        return Task.FromResult(new BaseResponseDTO<Role>
                        {
                            Status = 400,
                            Message = "Failed to add role.",
                            Data = null
                        });
                    }
                }
            }
        }catch (Exception ex)
        {
            return Task.FromResult(new BaseResponseDTO<Role>
            {
                Status = 500,
                Message = $"An error occurred while adding the role: {ex.Message}",
                Data = null
            });
        }
    }

    public Task<BaseResponseDTO<List<Role>>> GetAllRoles()
    {
        try
        {
            string query = "SELECT * FROM roles";
            using(var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using(var command = new MySqlCommand(query, connection))
                {
                    using(var reader = command.ExecuteReader())
                    {
                        var roles = new List<Role>();
                        while(reader.Read())
                        {
                            var role = new Role
                            {
                                RoleId = reader.GetGuid("id"),
                                RoleName = reader.GetString("name")
                            };
                            roles.Add(role);
                        }
                        return Task.FromResult(new BaseResponseDTO<List<Role>>
                        {
                            Status = 200,
                            Message = "Roles retrieved successfully.",
                            Data = roles
                        });
                    }
                }
            }

        }catch (Exception ex)
        {
            return Task.FromResult(new BaseResponseDTO<List<Role>>
            {
                Status = 500,
                Message = $"An error occurred while retrieving roles: {ex.Message}",
                Data = null
            });
        }
    }
}
