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
    public class StatusControllerTests
    {
        private readonly Mock<IStatusService> _mockService;
        private readonly StatusController _controller;

        public StatusControllerTests()
        {
            _mockService = new Mock<IStatusService>();//fake implementation of IStatusService and does not deal with the database
            _controller = new StatusController(_mockService.Object);//mock is then injected into StatusController
        }

        #region CSV Loader
        private static IEnumerable<object[]> LoadCsvData<T>(string fileName)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", fileName);//Reads a CSV file (e.g., StatusTestData.csv) located in the TestData folder
            if (!File.Exists(path))
                throw new FileNotFoundException($"CSV file not found: {path}");

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);//reads all the comma nad all punctuations
            var records = csv.GetRecords<T>().ToList();

            return records.Select(r => new object[] { r });//Returns a list of objects wrapped in object[]
        }
        #endregion

        #region Tests
        [Theory]//[Theory] means this test will run multiple times, once for each row in the CSV file.
        [MemberData(nameof(GetStatusTestData))]
        public async void StatusController_ShouldReturnExpectedResult(StatusCsvData data)
        {
            IActionResult result = null;

            if (data.Scenario.StartsWith("GetAll"))
            {
                IEnumerable<IssueStatus> expectedStatuses = new List<IssueStatus>();
                if (!string.IsNullOrEmpty(data.ExpectedStatusesJson))
                    expectedStatuses = JsonConvert.DeserializeObject<IEnumerable<IssueStatus>>(data.ExpectedStatusesJson)!;

                _mockService.Setup(s => s.GetStatusAsync())
                            .ReturnsAsync(expectedStatuses);

                result = await _controller.GetAll();

                var okResult = Assert.IsType<OkObjectResult>(result);
                var statuses = Assert.IsAssignableFrom<IEnumerable<IssueStatus>>(okResult.Value);
                Assert.Equal(expectedStatuses.Count(), statuses.Count());
            }
            else if (data.Scenario.StartsWith("GetById"))
            {
                IssueStatus? expectedStatus = null;
                if (!string.IsNullOrEmpty(data.ExpectedStatusJson))
                    expectedStatus = JsonConvert.DeserializeObject<IssueStatus>(data.ExpectedStatusJson);

                _mockService.Setup(s => s.GetStatusByIdAsync(data.Id))
                            .ReturnsAsync(expectedStatus);

                result = await _controller.GetById(data.Id);

                if (expectedStatus == null)
                {
                    Assert.IsType<NotFoundResult>(result);
                }
                else
                {
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    var status = Assert.IsType<IssueStatus>(okResult.Value);
                    Assert.Equal(expectedStatus.StatusId, status.StatusId);
                    Assert.Equal(expectedStatus.Name, status.Name);
                }
            }
        }

        public static IEnumerable<object[]> GetStatusTestData() =>
            LoadCsvData<StatusCsvData>("StatusTestData.csv");
        #endregion
    }
}
