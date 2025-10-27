using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Threading.Tasks;
using Business.DTOs;
using Business.Service.Interface;
using PublicWorks.API.Controllers;
using PublicWorks.API.Tests.Models;
using System;
using Data.Model;

namespace PublicWorks.API.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockService = new Mock<IUserService>();
            _controller = new UserController(_mockService.Object);
        }

        #region CSV Helpers
        private static IEnumerable<UserTestData> GetUserCsvData()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "UserTestData.csv");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim
            };

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);

            // Null values handling for CSV converter
            csv.Context.TypeConverterOptionsCache.GetOptions<DateTime?>().NullValues.Add("");
            csv.Context.TypeConverterOptionsCache.GetOptions<string>().NullValues.Add("");

            return csv.GetRecords<UserTestData>().ToList();
        }

        public static IEnumerable<object[]> GetUserCsvDataAsMemberData()
        {
            foreach (var record in GetUserCsvData())
                yield return new object[] { record };
        }
        #endregion

        #region CreateUser Tests
        [Theory]
        [MemberData(nameof(GetUserCsvDataAsMemberData))]
        public async Task CreateUser_ShouldHandleAllScenarios(UserTestData data)
        {
            var dto = new UserDto
            {
                UserId = data.UserId,
                GoogleUserId = data.GoogleUserId,
                Name = data.Name,
                PasswordHash = data.PasswordHash,
                PhoneNumber = data.PhoneNumber,
                ProfilePicture = data.ProfilePicture,
                RoleId = data.RoleId,
                LastLoginAt = data.LastLoginAt.HasValue ? new DateTimeOffset(data.LastLoginAt.Value) : DateTimeOffset.UtcNow,
                CreatedAt = data.CreatedAt.HasValue ? new DateTimeOffset(data.CreatedAt.Value) : DateTimeOffset.UtcNow,
                IsActive = data.IsActive,
                Email = data.Email
            };

            _mockService.Setup(s => s.CreateUserAsync(It.IsAny<UserDto>()))
                        .Returns(Task.CompletedTask);

            var result = await _controller.Create(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedUserDto = Assert.IsAssignableFrom<UserDto>(createdResult.Value);

            Assert.Equal(data.Name, returnedUserDto.Name);
            Assert.Equal(data.Email, returnedUserDto.Email);
            Assert.Equal(data.IsActive, returnedUserDto.IsActive);

            _mockService.Verify(s => s.CreateUserAsync(It.Is<UserDto>(u => u.Name == dto.Name && u.Email == dto.Email)), Times.Once);
        }
        #endregion

        #region GetAll Tests
        [Fact]
        public async Task GetAll_ShouldReturnUsers()
        {
            var mockUsers = GetUserCsvData().Select(d => new User
            {
                UserId = d.UserId,
                Name = d.Name,
                Email = d.Email,
                IsActive = d.IsActive,
                 LastLoginAt = d.LastLoginAt.HasValue ? new DateTimeOffset(d.LastLoginAt.Value) : DateTimeOffset.UtcNow,
                CreatedAt = d.CreatedAt.HasValue ? new DateTimeOffset(d.CreatedAt.Value) : DateTimeOffset.UtcNow,
            }).ToList();

            _mockService.Setup(s => s.GetUsersAsync()).ReturnsAsync(mockUsers);

            var result = await _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);

            Assert.Equal(mockUsers.Count, users.Count());
        }
        #endregion

        #region GetById Tests
        [Theory]
        [MemberData(nameof(GetUserCsvDataAsMemberData))]
        public async Task GetById_ShouldReturnUser_WhenExists(UserTestData data)
        {
            var mockUser = new User
            {
                UserId = data.UserId,
                Name = data.Name,
                Email = data.Email,
                IsActive = data.IsActive,
             LastLoginAt = data.LastLoginAt.HasValue ? new DateTimeOffset(data.LastLoginAt.Value) : DateTimeOffset.UtcNow,
                CreatedAt = data.CreatedAt.HasValue ? new DateTimeOffset(data.CreatedAt.Value) : DateTimeOffset.UtcNow,
            };

            _mockService.Setup(s => s.GetUserByIdAsync(data.UserId)).ReturnsAsync(mockUser);

            var result = await _controller.GetById(data.UserId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<User>(okResult.Value);

            Assert.Equal(data.UserId, returnedUser.UserId);
            Assert.Equal(data.Name, returnedUser.Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNotExists()
        {
            _mockService.Setup(s => s.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync((User?)null);

            var result = await _controller.GetById(-999);

            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region GetProfile Tests
        [Theory]
        [MemberData(nameof(GetUserCsvDataAsMemberData))]
        public void GetProfile_ShouldReturnClaims(UserTestData data)
        {
            var controller = new UserController(_mockService.Object);

            var claimUserId = data.ClaimUserId ?? "1";
            var claimName = data.ClaimName ?? "TestUser";

            var user = new System.Security.Claims.ClaimsPrincipal(
                new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, claimUserId),
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, claimName)
                    }, "mock")
            );

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = controller.GetProfile();
            var okResult = Assert.IsType<OkObjectResult>(result);
            dynamic response = okResult.Value!;

            Assert.Equal("Welcome!", response.message);
            Assert.Equal(claimUserId, response.userId);
            Assert.Equal(claimName, response.name);
        }
        #endregion
    }
}
