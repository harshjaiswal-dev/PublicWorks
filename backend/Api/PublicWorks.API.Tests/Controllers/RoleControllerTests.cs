using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Business.Service.Interface;
using Data.Model;
using PublicWorks.API.Controllers;
using PublicWorks.API.Tests.Models;

namespace PublicWorks.API.Tests.Controllers
{
    public class RoleControllerTests
    {
        private readonly Mock<IRoleService> _mockService;
        private readonly RoleController _controller;

        public RoleControllerTests()
        {
            _mockService = new Mock<IRoleService>();
            _controller = new RoleController(_mockService.Object);
        }

        #region CSV Helpers
        private static IEnumerable<RoleTestData> GetRoleCsvData()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "RoleTestData.csv");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim
            };

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);

            csv.Context.TypeConverterOptionsCache.GetOptions<string>().NullValues.Add("");

            return csv.GetRecords<RoleTestData>().ToList();
        }

        public static IEnumerable<object[]> GetRoleCsvDataAsMemberData()
        {
            foreach (var record in GetRoleCsvData())
                yield return new object[] { record };
        }
        #endregion

        #region GetAll Tests
        [Theory]
        [MemberData(nameof(GetRoleCsvDataAsMemberData))]
        public async Task GetAll_ShouldReturnAllRoles_ForEachCsvRecord(RoleTestData data)
        {
            var mockRoles = new List<Role>
    {
        new Role
        {
            RoleId = data.RoleId,
            Name = data.Name,
            Description = data.Description
        }
    };

            _mockService.Setup(s => s.GetRoleAsync()).ReturnsAsync(mockRoles);

            var result = await _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var roles = Assert.IsAssignableFrom<IEnumerable<Role>>(okResult.Value);

            Assert.Single(roles);
            Assert.Equal(data.RoleId, roles.First().RoleId);
        }
        #endregion


        #region GetById Tests
        [Theory]
        [MemberData(nameof(GetRoleCsvDataAsMemberData))]
        public async Task GetById_ShouldReturnRole_WhenExists(RoleTestData data)
        {
            var mockRole = new Role
            {
                RoleId = data.RoleId,
                Name = data.Name,
                Description = data.Description
            };

            _mockService.Setup(s => s.GetRoleByIdAsync(data.RoleId)).ReturnsAsync(mockRole);

            var result = await _controller.GetById(data.RoleId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRole = Assert.IsType<Role>(okResult.Value);

            Assert.Equal(data.RoleId, returnedRole.RoleId);
            Assert.Equal(data.Name, returnedRole.Name);
        }

        [Fact]
        
        public async Task GetById_ShouldReturnNotFound_WhenRoleDoesNotExist()
        {
            _mockService.Setup(s => s.GetRoleByIdAsync(It.IsAny<int>())).ReturnsAsync((Role?)null);

            var result = await _controller.GetById(-999);

            Assert.IsType<NotFoundResult>(result);
        }
        #endregion
    }

}

