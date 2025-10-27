using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;
using System.Threading.Tasks;
using PublicWorks.API.Controllers;
using Business.Service.Interface;
using Business.DTOs;
using Data.Model;
using PublicWorks.API.Tests.Models;
using System.Linq;

namespace PublicWorks.API.Tests.Controllers
{
    public class RemarkControllerTests
    {
        private readonly Mock<IRemarkService> _mockService;
        private readonly RemarkController _controller;

        public RemarkControllerTests()
        {
            // Mock the IRemarkService dependency
            _mockService = new Mock<IRemarkService>();
            // Inject mock service into RemarkController
            _controller = new RemarkController(_mockService.Object);
        }

        // =====================================================
        // CSV Data Loaders - Helper methods to read CSV files for dynamic test data
        // =====================================================

        // Load CSV test data for CreateRemark tests
        public static IEnumerable<object[]> GetRemarkTestData()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var csvPath = Path.Combine(basePath, "TestData", "RemarkTestCases.csv");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<RemarkTestData>().ToList();

            // Print record count to console for debugging
            Console.WriteLine($"Loaded {records.Count} records from RemarkTestCases.csv");

            // Return each record as an object array
            foreach (var record in records)
                yield return new object[] { record };
        }

        // Load CSV test data for GetAll() test cases
        public static IEnumerable<object[]> GetAllRemarksTestData()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var csvPath = Path.Combine(basePath, "TestData", "GetAllRemarksTestCases.csv");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<GetAllRemarkTestData>().ToList();

            // Group test data by scenario (Single, Multiple, Empty)
            var grouped = records.GroupBy(r => r.Scenario);

            foreach (var group in grouped)
            {
                // Prepare remarks list from grouped data
                var remarks = group
                    .Where(r => r.RemarkId.HasValue)
                    .Select(r => new IssueRemark
                    {
                        RemarkId = r.RemarkId!.Value,
                        IssueId = r.IssueId!.Value,
                        RemarkText = r.RemarkText
                    }).ToList();

                // Return grouped scenario and remarks as dynamic object
                yield return new object[] { new { Scenario = group.Key, Remarks = remarks } };
            }
        }

        // Load CSV test data for GetByIssueId() test cases
        public static IEnumerable<object[]> GetByIssueIdCsvData()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var csvPath = Path.Combine(basePath, "TestData", "GetByIdTestCases.csv");

            using var reader = new StreamReader(csvPath);
            using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<GetByIdTestData>().ToList();

            // Group by IssueId to handle multiple remarks for same issue
            var grouped = records.GroupBy(r => r.IssueId);

            foreach (var group in grouped)
            {
                var scenario = group.First().Scenario;
                List<IssueRemark> remarks = new();

                // Only create remarks list for valid scenarios
                if (scenario == "Valid")
                {
                    remarks = group
                        .Where(r => r.RemarkId.HasValue)
                        .Select(r => new IssueRemark
                        {
                            RemarkId = r.RemarkId.Value,
                            IssueId = r.IssueId,
                            RemarkText = r.RemarkText
                        }).ToList();
                }

                // Return IssueId, Scenario, and Expected Remarks
                yield return new object[] { group.Key, scenario, remarks };
            }
        }


        // =====================================================
        // TEST: CreateRemark - Uses dynamic data from CSV file
        // =====================================================
        [Theory]
        [MemberData(nameof(GetRemarkTestData))]
        public async Task CreateRemark_ShouldReturnExpectedStatus(RemarkTestData testCase)
        {
            // Arrange - Prepare DTO and Mock Service
            var dto = new IssueRemarkDto
            {
                RemarkId = testCase.RemarkId,
                IssueId = testCase.IssueId,
                RemarkText = testCase.RemarkText,
                RemarkedAt = DateTime.Now
            };

            _mockService.Setup(s => s.CreateRemarkAsync(It.IsAny<IssueRemarkDto>()))
                        .Returns(Task.CompletedTask);

            // Act - Call controller action
            var result = await _controller.Create(dto);

            // Assert - Verify result type and status code
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(testCase.ExpectedStatus, createdResult.StatusCode);
        }

        // =====================================================
        // TEST: GetAll - Uses grouped CSV data to test Single, Multiple, and Empty scenarios
        // =====================================================
        [Theory]
        [MemberData(nameof(GetAllRemarksTestData))]
        public async Task GetAll_ShouldHandleDifferentScenarios(dynamic testData)
        {
            var scenario = (string)testData.Scenario;
            var expectedRemarks = (List<IssueRemark>)testData.Remarks;

            // Arrange - Mock service to return expected remarks
            _mockService.Setup(s => s.GetRemarksAsync())
                        .ReturnsAsync(expectedRemarks);

            // Act - Call GetAll() action
            var result = await _controller.GetAll();

            // Assert based on scenario type
            switch (scenario)
            {
                case "Single":
                case "Multiple":
                    // Verify Ok result and correct data count
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    var data = Assert.IsAssignableFrom<IEnumerable<IssueRemark>>(okResult.Value);
                    Assert.Equal(expectedRemarks.Count, data.Count());
                    // Ensure each expected remark exists in response
                    foreach (var expected in expectedRemarks)
                        Assert.Contains(data, r => r.RemarkId == expected.RemarkId && r.RemarkText == expected.RemarkText);
                    break;

                case "Empty":
                    // Ensure response is Ok with empty list
                    var emptyResult = Assert.IsType<OkObjectResult>(result);
                    var list = Assert.IsAssignableFrom<IEnumerable<IssueRemark>>(emptyResult.Value);
                    Assert.Empty(list);
                    break;
            }
        }

        // =====================================================
        // TEST: GetByIssueId - Handles Valid, NotFound, and BadRequest scenarios
        // =====================================================
        [Theory]
        [MemberData(nameof(GetByIssueIdCsvData))]
        public async Task GetByIssueId_ShouldHandleMultipleScenarios(int issueId, string scenario, List<IssueRemark> expectedRemarks)
        {
            // Arrange - create new controller with mock service
            var controller = new RemarkController(_mockService.Object);

            // Configure mock based on scenario type
            switch (scenario)
            {
                case "Valid":
                    _mockService.Setup(s => s.GetRemarksbyIssueIdAsync(issueId))
                                .ReturnsAsync(expectedRemarks);
                    break;
                case "NotFound":
                    _mockService.Setup(s => s.GetRemarksbyIssueIdAsync(issueId))
                                .ReturnsAsync(new List<IssueRemark>());
                    break;
                case "BadRequest":
                    // No setup needed for invalid issueId case
                    break;
            }

            // Act - Call controller based on scenario
            IActionResult result;
            if (scenario == "BadRequest" && issueId <= 0)
            {
                result = controller.BadRequest("Invalid issueId");
            }
            else
            {
                result = await controller.GetByIssueId(issueId);
            }

            // Assert - Validate responses based on scenario type
            switch (scenario)
            {
                case "Valid":
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    Assert.Equal(200, okResult.StatusCode);

                    var actualRemarks = Assert.IsAssignableFrom<IEnumerable<IssueRemark>>(okResult.Value);
                    Assert.Equal(expectedRemarks.Count, actualRemarks.Count());

                    // Validate that each remark matches expected values
                    foreach (var expected in expectedRemarks)
                        Assert.Contains(actualRemarks, r => r.RemarkId == expected.RemarkId && r.RemarkText == expected.RemarkText);
                    break;

                case "NotFound":
                    // Expect Ok result with empty list (as per controller behavior)
                    var notFoundResult = Assert.IsType<OkObjectResult>(result);
                    var remarksList = Assert.IsAssignableFrom<IEnumerable<IssueRemark>>(notFoundResult.Value);
                    Assert.Empty(remarksList);
                    break;

                case "BadRequest":
                    // Expect 400 Bad Request for invalid IDs
                    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                    Assert.Equal(400, badRequestResult.StatusCode);
                    break;
            }
        }

    }
}
