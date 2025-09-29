using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Role>> GetRoleAsync()
        {
            return await _unitOfWork.RoleRepository.GetAllAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _unitOfWork.RoleRepository.GetByIdAsync(id);
        }

        // public async Task CreateRoleAsync(RoleDto dto)
        // {
        //     var roles = new Role()
        //     {
        //         RoleId =dto.RoleId,
        //         Name=dto.Name,
        //         Description=dto.Description
             
        //     };

        //     await _unitOfWork.RoleRepository.AddAsync(roles);
        //     await _unitOfWork.SaveAsync();
        // }

        // public async Task UpdateRoleAsync(int id, RoleDto dto)
        // {
        //     var roles = new Role()
        //     {
        //        RoleId=dto.RoleId,
        //         Name=dto.Name,
        //         Description=dto.Description
        //     };

        //     await _unitOfWork.RoleRepository.UpdateAsync(id, roles);
        //     await _unitOfWork.SaveAsync();
        // }

        // public async Task DeleteRoleAsync(int id)
        // {
        //     await _unitOfWork.RoleRepository.DeleteAsync(id);
        //     await _unitOfWork.SaveAsync();
        // }
    }
}