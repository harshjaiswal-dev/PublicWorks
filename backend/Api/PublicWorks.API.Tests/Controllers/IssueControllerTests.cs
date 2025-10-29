using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;
using System.Linq;
using System.Threading.Tasks;
using PublicWorks.API.Controllers;
using Business.Service.Interface;
using Business.DTOs;
using Data.Model;
using PublicWorks.API.Tests.Models;
using PublicWorks.API.Helpers;

namespace PublicWorks.API.Tests.Controllers
{
    public class IssueControllerTests
    {
        private readonly Mock<IIssueService> _mockService;
        private readonly Mock<IUserHelper> _mockUserHelper;
        private readonly IssueController _controller;

        public IssueControllerTests()
        {
            _mockService = new Mock<IIssueService>();
            _mockUserHelper = new Mock<IUserHelper>();
            _mockUserHelper.Setup(u => u.GetLoggedInUserId()).Returns(101);

            _controller = new IssueController(_mockService.Object, _mockUserHelper.Object);
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

        public static IEnumerable<object[]> GetAllTestData()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GetAllIssuesTestCases.csv");
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<GetAllIssuesTestData>().ToList();

            var grouped = records.GroupBy(r => r.Scenario);

            foreach (var group in grouped)
            {
                List<Issue> issues = new();

                if (group.Key != "Empty")
                {
                    issues = group
                        .Where(r => r.Id.HasValue)
                        .Select(r => new Issue
                        {
                            IssueId = r.Id.Value,
                            StatusId = r.StatusId ?? 0,
                            PriorityId = r.PriorityId ?? 0,
                            IssueCategoryId = r.CategoryId ?? 0,
                            Description = r.Description
                        }).ToList();
                }

                yield return new object[] { new { Scenario = group.Key, Issues = issues } };
            }
        }

        public static IEnumerable<object[]> GetByIdTestData()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GetIssueByIdTestCases.csv");
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<GetIssueByIdTestData>().ToList();

            foreach (var record in records)
            {
                Issue? issue = null;

                if (record.Scenario == "Valid")
                {
                    issue = new Issue
                    {
                        IssueId = record.Id,
                        StatusId = record.StatusId,
                        PriorityId = record.PriorityId,
                        IssueCategoryId = record.CategoryId,
                        Description = record.Description
                    };
                }

                yield return new object[] { record.Id, record.Scenario, issue };
            }
        }

        // =====================================================
        // CSV Data Loader for GetIssueImages test
        // =====================================================
        public static IEnumerable<object[]> GetIssueImagesCsvData()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var csvPath = Path.Combine(basePath, "TestData", "GetIssueImagesTestCases.csv");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<GetIssueImagesTestData>().ToList();

            // Group by Scenario (Valid, Empty, NotFound)
            var grouped = records.GroupBy(r => r.Scenario);

            foreach (var group in grouped)
            {
                var issueId = group.First().IssueId;
                var scenario = group.Key;

                var images = group
                    .Where(r => r.ImageId.HasValue)
                    .Select(r => new IssueImageDto
                    {
                        ImageId = r.ImageId.Value,
                        IssueId = r.IssueId,
                        ImagePath = r.ImagePath,
                        //UploadedAt = r.UploadedAt
                    }).ToList();

                yield return new object[] { issueId, scenario, images };
            }
        }



        [Theory]
        [MemberData(nameof(GetAllTestData))]
        public async Task GetAll_ShouldHandleDifferentScenarios(dynamic testData)
        {
            var scenario = (string)testData.Scenario;
            var expectedIssues = (List<Issue>)testData.Issues;

            // Arrange
            _mockService.Setup(s => s.GetIssuesAsync(null, null))
                        .ReturnsAsync(expectedIssues);

            // Act
            var result = await _controller.GetAll(null, null);

            // Assert
            switch (scenario)
            {
                case "Single":
                case "Multiple":
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    var data = Assert.IsAssignableFrom<IEnumerable<Issue>>(okResult.Value);
                    Assert.Equal(expectedIssues.Count, data.Count());

                    foreach (var expected in expectedIssues)
                        Assert.Contains(data, i => i.IssueId == expected.IssueId && i.Description == expected.Description);
                    break;

                case "Empty":
                    var emptyResult = Assert.IsType<OkObjectResult>(result);
                    var list = Assert.IsAssignableFrom<IEnumerable<Issue>>(emptyResult.Value);
                    Assert.Empty(list);
                    break;
            }
        }


        #region GetById Tests
        [Theory]
        [MemberData(nameof(GetByIdTestData))]
        public async Task GetById_ShouldHandleMultipleScenarios(int issueId, string scenario, Issue? expectedIssue)
        {
            // Arrange
            switch (scenario)
            {
                case "Valid":
                    _mockService.Setup(s => s.GetIssueByIdAsync(issueId))
                                .ReturnsAsync(expectedIssue);
                    break;

                case "NotFound":
                    _mockService.Setup(s => s.GetIssueByIdAsync(issueId))
                                .ReturnsAsync((Issue?)null);
                    break;

                case "BadRequest":
                    // no setup needed for invalid ID
                    break;
            }

            // Act
            IActionResult result;
            if (scenario == "BadRequest" && issueId <= 0)
            {
                result = await _controller.GetById(issueId);
            }
            else
            {
                result = await _controller.GetById(issueId);
            }

            // Assert
            switch (scenario)
            {
                case "Valid":
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    Assert.Equal(200, okResult.StatusCode);

                    var actualIssue = Assert.IsType<Issue>(okResult.Value);
                    Assert.Equal(expectedIssue.IssueId, actualIssue.IssueId);
                    Assert.Equal(expectedIssue.Description, actualIssue.Description);
                    break;

                case "NotFound":
                    Assert.IsType<NotFoundResult>(result);
                    break;

                case "BadRequest":
                    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                    Assert.Equal(400, badRequestResult.StatusCode);
                    break;
            }
        }



        #endregion



        #region SubmitIssue Tests
        [Theory]
        [MemberData(nameof(SubmitTestData))]
        public async Task SubmitIssue_ShouldReturnExpectedResponse(SubmitIssueTestData testCase)
        {
            // Arrange
            var dto = new IssueCreateDto
            {
                UserId = testCase.UserId,
                CategoryId = testCase.CategoryId,
                Latitude = testCase.Latitude,
                Longitude = testCase.Longitude,
                Description = testCase.Description
            };

            _mockService.Setup(s => s.SubmitIssueAsync(It.IsAny<IssueCreateDto>()))
                        .ReturnsAsync(testCase.ExpectedIssueId);

            // Act
            var result = await _controller.SubmitIssue(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<SubmitIssueResponse>(okResult.Value);

            Assert.Equal(testCase.ExpectedSuccess, response.Success);
            Assert.Equal(testCase.ExpectedIssueId, response.IssueId);
        }


        public static IEnumerable<object[]> SubmitTestData =>
            LoadCsvData<SubmitIssueTestData>("SubmitIssueTestCases.csv");
        #endregion

        #region UpdateIssue Tests
        [Theory]
        [MemberData(nameof(UpdateIssueTestDataList))]
        public async Task UpdateIssue_ShouldReturnCorrectResult(UpdateIssueTestData testData)
        {
            // Arrange
            if (testData.ThrowsException)
            {
                _mockService.Setup(s => s.UpdateIssueAsync(
                        testData.IssueId,
                        testData.StatusId,
                        testData.PriorityId,
                        testData.CategoryId))
                    .ThrowsAsync(new Exception("Boom"));
            }
            else if (!testData.Exists)
            {
                _mockService.Setup(s => s.UpdateIssueAsync(
                        testData.IssueId,
                        testData.StatusId,
                        testData.PriorityId,
                        testData.CategoryId))
                    .ReturnsAsync((Issue?)null);
            }
            else
            {
                // Create an Issue that reflects what the service would return after update.
                // Note: if a field is 0 in the input, the service leaves it unchanged;
                // to simulate "unchanged" fields, set original values you expect to see.
                var returnedIssue = new Issue
                {
                    IssueId = testData.IssueId,
                    StatusId = (testData.StatusId != 0 ? testData.StatusId : testData.ExpectedStatusId),
                    PriorityId = (testData.PriorityId != 0 ? testData.PriorityId : 1),
                    IssueCategoryId = (testData.CategoryId != 0 ? testData.CategoryId : 1)
                };

                _mockService.Setup(s => s.UpdateIssueAsync(
                        testData.IssueId,
                        testData.StatusId,
                        testData.PriorityId,
                        testData.CategoryId))
                    .ReturnsAsync(returnedIssue);
            }

            var dto = new UpdateIssueDto
            {
                StatusId = testData.StatusId,
                PriorityId = testData.PriorityId,
                CategoryId = testData.CategoryId
            };

            // Act
            var result = await _controller.UpdateIssue(testData.IssueId, dto);

            // Assert - by ExpectedStatusCode
            if (testData.ExpectedStatusCode == 200)
            {
                var ok = Assert.IsType<OkObjectResult>(result);
                Assert.Equal(200, ok.StatusCode ?? 200);

                // controller returns UpdateIssueResponse with IssueId and StatusId
                var response = Assert.IsType<UpdateIssueResponse>(ok.Value);
                Assert.Equal(testData.ExpectedIssueId == 0 ? testData.IssueId : testData.ExpectedIssueId, response.IssueId);
                Assert.Equal(testData.ExpectedStatusId == 0 ? dto.StatusId != 0 ? dto.StatusId : response.StatusId : testData.ExpectedStatusId,
                             response.StatusId);
                // Optional: if you add Message or Success in controller's Ok response, check them here.
            }
            else if (testData.ExpectedStatusCode == 404)
            {
                var notFound = Assert.IsType<NotFoundObjectResult>(result);
                Assert.Equal(404, notFound.StatusCode);

                var response = Assert.IsType<UpdateIssueResponse>(notFound.Value);
                Assert.Equal("Issue not found", response.Message);
            }
            else if (testData.ExpectedStatusCode == 500)
            {
                var objResult = Assert.IsType<ObjectResult>(result);
                Assert.Equal(500, objResult.StatusCode);

                // The controller returns an anonymous object: new { message = "Internal server error" }
                var value = objResult.Value;
                var messageProp = value.GetType().GetProperty("message");
                Assert.NotNull(messageProp);
                var actualMsg = messageProp.GetValue(value) as string;
                Assert.Equal("Internal server error", actualMsg);
            }
            else
            {
                Assert.True(false, $"Unexpected ExpectedStatusCode {testData.ExpectedStatusCode}");
            }
        }

        public static IEnumerable<object[]> UpdateIssueTestDataList =>
            LoadCsvData<UpdateIssueTestData>("UpdateIssueTestCases.csv");
        #endregion


[Theory]
        [MemberData(nameof(GetIssueImagesCsvData))]
        public async Task GetIssueImages_ShouldHandleDifferentScenarios(int issueId, string scenario, List<IssueImageDto> expectedImages)
        {
            // Arrange - Setup mock based on scenario
            switch (scenario)
            {
                case "Valid":
                    _mockService.Setup(s => s.GetIssueImagesAsync(issueId))
                                .ReturnsAsync(expectedImages);
                    break;

                case "Empty":
                    _mockService.Setup(s => s.GetIssueImagesAsync(issueId))
                                .ReturnsAsync(new List<IssueImageDto>());
                    break;

                case "NotFound":
                    _mockService.Setup(s => s.GetIssueImagesAsync(issueId))
                                .ReturnsAsync((IEnumerable<IssueImageDto>)null);
                    break;
            }

            // Act
            var result = await _controller.GetIssueImages(issueId);

            // Assert
            switch (scenario)
            {
                case "Valid":
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    var actualImages = Assert.IsAssignableFrom<IEnumerable<IssueImageDto>>(okResult.Value);
                    Assert.Equal(expectedImages.Count, actualImages.Count());
                    foreach (var expected in expectedImages)
                        Assert.Contains(actualImages, i => i.ImageId == expected.ImageId && i.ImagePath == expected.ImagePath);
                    break;

                case "Empty":
                    var notFoundEmpty = Assert.IsType<NotFoundObjectResult>(result);
                    var responseEmpty = Assert.IsType<IssueImagesResponse>(notFoundEmpty.Value);
                    Assert.Equal("No images found for this issue.", responseEmpty.Message);
                    Assert.Null(responseEmpty.Data);
                    break;

                case "NotFound":
                    var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
                    var response = Assert.IsType<IssueImagesResponse>(notFoundResult.Value);
                    Assert.Equal("No images found for this issue.", response.Message);
                    Assert.Null(response.Data);
                    break;
            }
        }

        
    }
}
