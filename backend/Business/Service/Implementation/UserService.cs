using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;

namespace Business.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUoW _unitOfWork;

        public UserService(IUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task CreateUserAsync(UserDto dto)
        {
            var user = new User()
            {
                UserId = dto.UserId,
                GoogleUserId = dto.GoogleUserId,
                Name = dto.Name,
                PasswordHash = dto.PasswordHash,
                ProfilePicture = dto.ProfilePicture,
                RoleId = dto.RoleId,
                LastLoginAt = dto.LastLoginAt,
                CreatedAt = dto.CreatedAt,
                IsActive = dto.IsActive
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserAsync(int id, UserDto dto)
        {
            var user = new User()
            {
                UserId = dto.UserId,
                GoogleUserId = dto.GoogleUserId,
                Name = dto.Name,
                PasswordHash = dto.PasswordHash,
                ProfilePicture = dto.ProfilePicture,
                RoleId = dto.RoleId,
                LastLoginAt = dto.LastLoginAt,
                CreatedAt = dto.CreatedAt,
                IsActive = dto.IsActive
            };

            await _unitOfWork.UserRepository.UpdateAsync(id, user);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            await _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}