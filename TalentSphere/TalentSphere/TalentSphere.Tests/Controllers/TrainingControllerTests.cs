using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for TrainingController.GetById endpoint
    [TestFixture]
    public class TrainingControllerTests
    {
        private Mock<ITrainingService> _trainingServiceMock;
        private TrainingController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake ITrainingService
            _trainingServiceMock = new Mock<ITrainingService>();

            // AuditLogHelper is not used by GetById, so pass null!
            _controller = new TrainingController(
                _trainingServiceMock.Object,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetById_TrainingExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake training
            var fakeTraining = new TrainingResponseDTO { TrainingID = 1 };
            _trainingServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeTraining);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_TrainingDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _trainingServiceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((TrainingResponseDTO?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
