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
    public class MessageControllerTests
    {
        private readonly Mock<IMessageService> _mockService;
        private readonly MessageController _controller;

        public MessageControllerTests()
        {
            _mockService = new Mock<IMessageService>();
            _controller = new MessageController(_mockService.Object);
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
        [MemberData(nameof(GetMessageTestData))]
        public async void MessageController_ShouldReturnExpectedResult(MessageCsvData data)
        {
            IActionResult result = null;

            if (data.Scenario.StartsWith("GetAll"))
            {
                IEnumerable<IssueMessage> expectedMessages = new List<IssueMessage>();
                if (!string.IsNullOrEmpty(data.ExpectedMessagesJson))
                    expectedMessages = JsonConvert.DeserializeObject<IEnumerable<IssueMessage>>(data.ExpectedMessagesJson)!;

                _mockService.Setup(s => s.GetMessagesAsync())
                            .ReturnsAsync(expectedMessages);

                result = await _controller.GetAll();

                var okResult = Assert.IsType<OkObjectResult>(result);
                var messages = Assert.IsAssignableFrom<IEnumerable<IssueMessage>>(okResult.Value);
                Assert.Equal(expectedMessages.Count(), messages.Count());
            }
            else if (data.Scenario.StartsWith("GetById"))
            {
                IssueMessage? expectedMessage = null;
                if (!string.IsNullOrEmpty(data.ExpectedMessageJson))
                    expectedMessage = JsonConvert.DeserializeObject<IssueMessage>(data.ExpectedMessageJson);

                _mockService.Setup(s => s.GetMessageByIdAsync(data.Id))
                            .ReturnsAsync(expectedMessage);

                result = await _controller.GetById(data.Id);

                if (expectedMessage == null)
                {
                    Assert.IsType<NotFoundResult>(result);
                }
                else
                {
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    var message = Assert.IsType<IssueMessage>(okResult.Value);
                    Assert.Equal(expectedMessage.MessageId, message.MessageId);
                    Assert.Equal(expectedMessage.IssueId, message.IssueId);
                    Assert.Equal(expectedMessage.Subject, message.Subject);
                    Assert.Equal(expectedMessage.Body, message.Body);
                }
            }
        }

        public static IEnumerable<object[]> GetMessageTestData() =>
            LoadCsvData<MessageCsvData>("MessageTestData.csv");
        #endregion
    }
}
