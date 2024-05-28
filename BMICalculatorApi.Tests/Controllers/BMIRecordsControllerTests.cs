using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BMICalculatorApi.Controllers;
using BMICalculatorApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMICalculatorApi.Tests.Controllers
{
    public class BmiRecordsControllerTests
    {
        private readonly Mock<IBMIRecordRepository> _mockRepo;
        private readonly Mock<ILogger<BmiController>> _mockLogger;
        private readonly BmiController _controller;

        public BmiRecordsControllerTests()
        {
            _mockRepo = new Mock<IBMIRecordRepository>();
            _mockLogger = new Mock<ILogger<BmiController>>();
            _controller = new BmiController(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetBmiRecords_ReturnsAllRecords()
        {
            var bmiRecords = new List<BMIRecord>
            {
                new BMIRecord { Id = 1, Name = "Test1", Height = 1.75, Weight = 70, Bmi = 22.86 },
                new BMIRecord { Id = 2, Name = "Test2", Height = 1.65, Weight = 60, Bmi = 22.04 }
            };

            _mockRepo.Setup(repo => repo.GetAllRecordsAsync()).ReturnsAsync(bmiRecords);

            var result = await _controller.GetBmiRecords();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<BMIRecord>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<BMIRecord>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetBmiRecord_ReturnsRecordById()
        {
            var bmiRecord = new BMIRecord { Id = 1, Name = "Test1", Height = 1.75, Weight = 70, Bmi = 22.86 };

            _mockRepo.Setup(repo => repo.GetRecordByIdAsync(1)).ReturnsAsync(bmiRecord);

            var result = await _controller.GetBmiRecord(1);

            var actionResult = Assert.IsType<ActionResult<BMIRecord>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<BMIRecord>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task PostBmiRecord_AddsRecord()
        {
            var bmiRecord = new BMIRecord { Id = 1, Name = "Test1", Height = 1.75, Weight = 70, Bmi = 22.86 };

            _mockRepo.Setup(repo => repo.AddRecordAsync(bmiRecord)).Returns(Task.CompletedTask);

            var result = await _controller.PostBmiRecord(bmiRecord);

            var actionResult = Assert.IsType<ActionResult<BMIRecord>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<BMIRecord>(createdAtActionResult.Value);
            Assert.Equal(bmiRecord.Id, returnValue.Id);
        }

        [Fact]
        public async Task PutBmiRecord_UpdatesRecord()
        {
            var bmiRecord = new BMIRecord { Id = 1, Name = "Test1", Height = 1.75, Weight = 70, Bmi = 22.86 };

            _mockRepo.Setup(repo => repo.UpdateRecordAsync(bmiRecord)).Returns(Task.CompletedTask);
            _mockRepo.Setup(repo => repo.RecordExistsAsync(1)).ReturnsAsync(true);

            var result = await _controller.PutBmiRecord(1, bmiRecord);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBmiRecord_DeletesRecord()
        {
            var bmiRecord = new BMIRecord { Id = 1, Name = "Test1", Height = 1.75, Weight = 70, Bmi = 22.86 };

            _mockRepo.Setup(repo => repo.GetRecordByIdAsync(1)).ReturnsAsync(bmiRecord);
            _mockRepo.Setup(repo => repo.DeleteRecordAsync(bmiRecord)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteBmiRecord(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
