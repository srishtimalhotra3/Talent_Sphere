using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for AuditLogsController.GetById endpoint
    [TestFixture]
    public class AuditLogsControllerTests
    {
        private Mock<IAuditLogService> _auditLogServiceMock;
        private AuditLogsController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IAuditLogService
            _auditLogServiceMock = new Mock<IAuditLogService>();
            _controller = new AuditLogsController(_auditLogServiceMock.Object);
        }

        [Test]
        public async Task GetById_AuditLogExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake audit log
            var fakeLog = new AuditLogResponseDto { AuditID = 1 };
            _auditLogServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeLog);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_AuditLogDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _auditLogServiceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((AuditLogResponseDto)null!);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
