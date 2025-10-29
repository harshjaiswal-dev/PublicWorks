using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Business.Service.Interface;
using Business.DTOs;
using Data.Model;
using PublicWorks.API.Controllers;
using PublicWorks.API.Tests.Models;
using Business.Service.Implementation;
using Microsoft.Extensions.Options;
using Business.Service.Settings;

namespace PublicWorks.API.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<IGoogleAuthService> _mockGoogleAuthService;
        private readonly JwtService _jwtService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _mockGoogleAuthService = new Mock<IGoogleAuthService>();

            // Create real JwtService with test settings
            var jwtSettings = Options.Create(new JwtSettings
            {
                Key = "TEST_SECRET_KEY_32_CHARACTERS_LONG!!",
                AccessTokenExpiryMinutes = 60,
                Issuer = "TestIssuer",
                Audience = "TestAudience"
            });

            _jwtService = new JwtService(jwtSettings);

            _controller = new AuthController(
                _mockGoogleAuthService.Object,
                _jwtService,
                _mockAuthService.Object
            );
        }

        #region CSV Loader
        private static IEnumerable<object[]> LoadCsvData<T>(string fileName)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", fileName);
            if (!File.Exists(path))
                throw new FileNotFoundException($"CSV file not found: {path}");

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<T>().ToList();

            return records.Select(r => new object[] { r });
        }
        #endregion

        #region AdminLogin & GoogleCallback Tests
        [Theory]
        [MemberData(nameof(GetAuthTestData))]
        public async void AuthController_ShouldReturnExpectedResult(AuthCsvData data)
        {
            IActionResult result = null;

            if (data.Scenario.StartsWith("ValidAdmin") || data.Scenario.StartsWith("InvalidAdmin") || data.Scenario.StartsWith("ValidUser"))
            {
                // Admin/User login scenario
                if (data.ExpectedStatus == 200)
                {
                    var user = new User
                    {
                        UserId = 1,
                        Name = data.Username,
                        RoleId = data.Role == "Admin" ? 1 : 2,
                        Email = $"{data.Username}@example.com",
                        ProfilePicture = "default.png"
                    };

                    _mockAuthService.Setup(a => a.AdminLoginAsync(data.Username, data.Password))
                                    .ReturnsAsync(user);
                }
                else
                {
                    _mockAuthService.Setup(a => a.AdminLoginAsync(data.Username, data.Password))
                                    .ReturnsAsync((User)null);
                }

                var loginDto = new AdminLoginDto
                {
                    Username = data.Username,
                    Password = data.Password
                };

                result = await _controller.AdminLogin(loginDto);
            }
            else if (data.Scenario.StartsWith("Google"))
            {
                // Google login scenario
                if (data.ExpectedStatus == 200)
                {
                    var user = new User
                    {
                        UserId = 1,
                        Name = "GoogleUser",
                        RoleId = 2,
                        ProfilePicture = "google.png"
                    };

                    _mockGoogleAuthService.Setup(g => g.HandleGoogleLoginAsync(data.Code))
                                          .ReturnsAsync(user);
                }
                else
                {
                    _mockGoogleAuthService.Setup(g => g.HandleGoogleLoginAsync(data.Code))
                                          .ThrowsAsync(new System.Exception("Invalid Google code"));
                }

                result = await _controller.GoogleCallback(data.Code);
            }

            // Assert response based on expected status
            switch (data.ExpectedStatus)
            {
                case 200:
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    Assert.Equal(200, okResult.StatusCode);
                    break;
                case 401:
                    var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
                    Assert.Equal(401, unauthorizedResult.StatusCode);
                    break;
                case 400:
                    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                    Assert.Equal(400, badRequestResult.StatusCode);
                    break;
            }
        }

        public static IEnumerable<object[]> GetAuthTestData() =>
            LoadCsvData<AuthCsvData>("AuthTestData.csv");
        #endregion
    }
}
