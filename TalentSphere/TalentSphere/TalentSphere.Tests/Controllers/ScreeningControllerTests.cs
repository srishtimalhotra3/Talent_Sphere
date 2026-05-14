using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for ScreeningController.GetById endpoint
    [TestFixture]
    public class ScreeningControllerTests
    {
        private Mock<IScreeningService> _screeningServiceMock;
        private ScreeningController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IScreeningService
            _screeningServiceMock = new Mock<IScreeningService>();

            // AuditLogHelper is not used by GetById, so pass null!
            _controller = new ScreeningController(
                _screeningServiceMock.Object,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetById_ScreeningExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake screening
            var fakeScreening = new ScreeningResponseDTO { ScreeningID = 1 };
            _screeningServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeScreening);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_ScreeningDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _screeningServiceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((ScreeningResponseDTO?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
