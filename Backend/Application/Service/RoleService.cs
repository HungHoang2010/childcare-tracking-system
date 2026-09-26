using System;
using Application.IService;
using Application.RequestDTO;
using Application.ResponseDTO;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Infrastructure.Repository;
namespace Application.Service;

public class RoleService : IRoleService
{
    private readonly string _connectionString;
    private readonly IRepository<Role> _roleRepository;
    public RoleService(IConfiguration configuration, IRepository<Role> roleRepository)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        _roleRepository = roleRepository;
    }

    public async Task<BaseResponseDTO<Role>> AddRole(AddRoleRequest request)
    {
        try
        {
            string sp = "sp_Roles_InsertRoles";
            var newRoleId = Guid.NewGuid();
            var parameters = new
            {
                pRoleID = newRoleId,
                pRoleName = request.RoleName,
                pRoleDescription = request.RoleDescription
            };
            await _roleRepository.ExecuteAsync(sp, parameters);
            return await Task.FromResult(new BaseResponseDTO<Role>
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
        }catch (Exception ex)
        {
            return await Task.FromResult(new BaseResponseDTO<Role>
            {
                Status = 500,
                Message = $"An error occurred while adding the role: {ex.Message}",
                Data = null
            });
        }
    }

    public async Task<BaseResponseDTO<List<Role>>> GetAllRoles()
    {
        try
        {
            string query = "SELECT * FROM roles";
            var result = await _roleRepository.GetAllAsync(query, new { });
            var roles = new List<Role>(result);
            return new BaseResponseDTO<List<Role>>
            {
                Status = 200,
                Message = "Roles retrieved successfully.",
                Data = roles
            };
        }catch (Exception ex)
        {
            return new BaseResponseDTO<List<Role>>
            {
                Status = 500,
                Message = $"An error occurred while retrieving roles: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<BaseResponseDTO<Role>> UpdateRole(UpdateRoleRequest request, Guid roleId)
    {
        try{
           string sp = "sp_Roles_UpdateRoleNameAndRoleDescriptions";
            var parameters = new
            {
                pRoleID = roleId,
                pRoleName = request.RoleName,
                pRoleDescription = request.RoleDescription
            };
            await _roleRepository.ExecuteAsync(sp, parameters);
            return await Task.FromResult(new BaseResponseDTO<Role>
            {
                Status = 200,
                Message = "Role updated successfully.",
                Data = new Role
                {
                    RoleId = roleId,
                    RoleName = request.RoleName,
                    RoleDescription = request.RoleDescription
                }
            });

        }catch (Exception ex)
        {
            return await Task.FromResult(new BaseResponseDTO<Role>
            {
                Status = 500,       
                Message = $"An error occurred while updating the role: {ex.Message}",
                Data = null
            });
        }
    }
}
