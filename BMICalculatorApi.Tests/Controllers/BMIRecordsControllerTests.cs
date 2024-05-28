using System.Collections.Generic;
using System.Threading.Tasks;
using BMICalculatorApi.Controllers;
using BMICalculatorApi.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BMICalculatorApi.Tests.Controllers
{
    public class BmiRecordsControllerTests
    {
        private readonly Mock<IBMIRecordRepository> _mockRepo;
        private readonly BMIRecordsController _controller;

        public BmiRecordsControllerTests()
        {
            _mockRepo = new Mock<IBMIRecordRepository>();
            _controller = new BMIRecordsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetBMIRecords_ReturnsOkResult_WithListOfRecords()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllRecordsAsync()).ReturnsAsync(GetTestRecords());

            // Act
            var result = await _controller.GetBMIRecords();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<BMIRecord>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        private List<BMIRecord> GetTestRecords()
        {
            return new List<BMIRecord>
            {
                new BMIRecord { Id = 1, Name = "John Doe", Height = 180, Weight = 75 },
                new BMIRecord { Id = 2, Name = "Jane Doe", Height = 160, Weight = 60 }
            };
        }
    }
}