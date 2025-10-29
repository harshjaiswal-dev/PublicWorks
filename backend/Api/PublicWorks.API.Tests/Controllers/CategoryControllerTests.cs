using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Business.Service.Interface;
using Data.Model;
using PublicWorks.API.Controllers;
using PublicWorks.API.Tests.Models;

namespace PublicWorks.API.Tests
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _mockService;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockService = new Mock<ICategoryService>();
            _controller = new CategoryController(_mockService.Object);
        }

        #region CSV Loader
        private static IEnumerable<object[]> LoadCsvData(string fileName)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", fileName);
            if (!File.Exists(path))
                throw new FileNotFoundException($"CSV file not found: {path}");

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CategoryTestData>().ToList();

            return records.Select(r => new object[] { r });
        }
        #endregion

        #region Tests
        [Theory]
        [MemberData(nameof(GetCategoryTestData))]
        public async void CategoryController_ShouldReturnExpectedResult(CategoryTestData data)
        {
            if (data.Scenario.StartsWith("GetAll"))
            {
                var expectedList = JsonConvert.DeserializeObject<List<IssueCategory>>(data.ExpectedCategoriesJson) ?? new List<IssueCategory>();
                _mockService.Setup(s => s.GetCategoryAsync())
                            .ReturnsAsync(expectedList);

                var result = await _controller.GetAll();
                var okResult = Assert.IsType<OkObjectResult>(result);
                var categories = Assert.IsAssignableFrom<IEnumerable<IssueCategory>>(okResult.Value);

                Assert.Equal(expectedList.Count, categories.Count());
            }
            else if (data.Scenario.StartsWith("GetById"))
            {
                IssueCategory? expectedCategory = null;
                if (!string.IsNullOrEmpty(data.ExpectedCategoryJson))
                    expectedCategory = JsonConvert.DeserializeObject<IssueCategory>(data.ExpectedCategoryJson);

                _mockService.Setup(s => s.GetCategoryByIdAsync(data.Id))
                            .ReturnsAsync(expectedCategory);

                var result = await _controller.GetById(data.Id);

                if (expectedCategory == null)
                {
                    Assert.IsType<NotFoundResult>(result);
                }
                else
                {
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    var category = Assert.IsType<IssueCategory>(okResult.Value);
                    Assert.Equal(expectedCategory.CategoryId, category.CategoryId);
                    Assert.Equal(expectedCategory.Name, category.Name);
                }
            }
        }

        public static IEnumerable<object[]> GetCategoryTestData() =>
            LoadCsvData("CategoryTestData.csv");
        #endregion
    }
}
