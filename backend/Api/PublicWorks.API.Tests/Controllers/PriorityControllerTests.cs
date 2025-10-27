using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Business.Service.Interface;
using Data.Model;
using PublicWorks.API.Controllers;
using PublicWorks.API.Tests.Models;
using Newtonsoft.Json;

namespace PublicWorks.API.Tests
{
    public class PriorityControllerTests
    {
        private readonly Mock<IPriorityService> _mockService;
        private readonly PriorityController _controller;

        public PriorityControllerTests()
        {
            _mockService = new Mock<IPriorityService>();
            _controller = new PriorityController(_mockService.Object);
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

        #region Tests
        [Theory]
        [MemberData(nameof(GetPriorityTestData))]
        public async void PriorityController_ShouldReturnExpectedResult(PriorityCsvData data)
        {
            IActionResult result = null;

            if (data.Scenario.StartsWith("GetAll"))
            {
                IEnumerable<IssuePriority> expectedPriorities = new List<IssuePriority>();
                if (!string.IsNullOrEmpty(data.ExpectedPrioritiesJson))
                    expectedPriorities = JsonConvert.DeserializeObject<IEnumerable<IssuePriority>>(data.ExpectedPrioritiesJson)!;

                _mockService.Setup(s => s.GetPriorityAsync())
                            .ReturnsAsync(expectedPriorities);

                result = await _controller.GetAll();

                var okResult = Assert.IsType<OkObjectResult>(result);
                var priorities = Assert.IsAssignableFrom<IEnumerable<IssuePriority>>(okResult.Value);
                Assert.Equal(expectedPriorities.Count(), priorities.Count());
            }
            else if (data.Scenario.StartsWith("GetById"))
            {
                IssuePriority? expectedPriority = null;
                if (!string.IsNullOrEmpty(data.ExpectedPriorityJson))
                    expectedPriority = JsonConvert.DeserializeObject<IssuePriority>(data.ExpectedPriorityJson);

                _mockService.Setup(s => s.GetPriorityByIdAsync(data.Id))
                            .ReturnsAsync(expectedPriority);

                result = await _controller.GetById(data.Id);

                if (expectedPriority == null)
                {
                    Assert.IsType<NotFoundResult>(result);
                }
                else
                {
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    var priority = Assert.IsType<IssuePriority>(okResult.Value);
                    Assert.Equal(expectedPriority.PriorityId, priority.PriorityId);
                    Assert.Equal(expectedPriority.Name, priority.Name);
                    Assert.Equal(expectedPriority.Description, priority.Description);
                }
            }
        }

        public static IEnumerable<object[]> GetPriorityTestData() =>
            LoadCsvData<PriorityCsvData>("PriorityTestData.csv");
        #endregion
    }
}
