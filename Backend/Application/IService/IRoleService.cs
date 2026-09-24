using System;
using Application.RequestDTO;
using Application.ResponseDTO;
using Domain.Models;

namespace Application.IService;

public interface IRoleService
{
    Task<BaseResponseDTO<List<Role>>> GetAllRoles();
    Task<BaseResponseDTO<Role>> AddRole(AddRoleRequest requests);
}