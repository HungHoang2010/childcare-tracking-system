using Application.IService;
using Application.RequestDTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAllRoles();
            return StatusCode(response.Status, response);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] AddRoleRequest request)
        {
            var role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = request.RoleName,
                RoleDescription = request.RoleDescription
            };
            var response = await _roleService.AddRole(request);
            return StatusCode(response.Status, response);
        }
        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(Guid roleId, [FromBody] UpdateRoleRequest request)
        {
            var response = await _roleService.UpdateRole(request, roleId);
            return StatusCode(response.Status, response);
        }
    }
}
