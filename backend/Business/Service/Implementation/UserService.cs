using Business.DTOs;
using Business.Service.Interface;
using Data.Model;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Business.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
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
            var passwordHasher = new PasswordHasher<User>();
            var user = new User()
            {
                UserId = dto.UserId,
                GoogleUserId = dto.GoogleUserId,
                Name = dto.Name,
               // PasswordHash = dto.PasswordHash,
                PhoneNumber = dto.PhoneNumber,
                ProfilePicture = dto.ProfilePicture,
                RoleId = dto.RoleId,
                LastLoginAt = dto.LastLoginAt,
                CreatedAt = dto.CreatedAt,
                IsActive = dto.IsActive,
                Email=dto.Email
            };
            user.PasswordHash = passwordHasher.HashPassword(user, dto.PasswordHash);
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();
        }

        // public async Task UpdateUserAsync(int id, UserDto dto)
        // {
        //     var user = new User()
        //     {
        //         UserId = dto.UserId,
        //         GoogleUserId = dto.GoogleUserId,
        //         Name = dto.Name,
        //         PasswordHash = dto.PasswordHash,
        //         ProfilePicture = dto.ProfilePicture,
        //         RoleId = dto.RoleId,
        //         LastLoginAt = dto.LastLoginAt,
        //         CreatedAt = dto.CreatedAt,
        //         IsActive = dto.IsActive
        //     };

          

        //     await _unitOfWork.UserRepository.UpdateAsync(id, user);
        //     await _unitOfWork.SaveAsync();
        // }

        // public async Task DeleteUserAsync(int id)
        // {
        //     await _unitOfWork.UserRepository.DeleteAsync(id);
        //     await _unitOfWork.SaveAsync();
        // }
    }
}