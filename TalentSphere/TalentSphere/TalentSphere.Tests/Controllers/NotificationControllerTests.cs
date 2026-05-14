using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for NotificationController.MarkRead endpoint
    [TestFixture]
    public class NotificationControllerTests
    {
        private Mock<INotificationService> _serviceMock;
        private NotificationController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake INotificationService
            _serviceMock = new Mock<INotificationService>();
            _controller = new NotificationController(_serviceMock.Object);
        }

        [Test]
        public async Task MarkRead_NotificationExists_ReturnsNoContent()
        {
            // Arrange: mock returns true (mark succeeded)
            _serviceMock.Setup(s => s.MarkAsReadAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.MarkRead(1);

            // Assert: 204 No Content
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task MarkRead_NotificationDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns false (notification not found)
            _serviceMock.Setup(s => s.MarkAsReadAsync(99)).ReturnsAsync(false);

            // Act
            var result = await _controller.MarkRead(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
