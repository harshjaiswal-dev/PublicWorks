using Business.DTOs;
using Data.Model;

namespace Business.Service.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoleAsync();
        Task<Role> GetRoleByIdAsync(int id);
        Task CreateRoleAsync(RoleDto roles);
        Task UpdateRoleAsync(int id, RoleDto roles);
        Task DeleteRoleAsync(int id);
    }
}