using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for AuditController.GetAuditById endpoint
    [TestFixture]
    public class AuditControllerTests
    {
        private Mock<IAuditService> _auditServiceMock;
        private AuditController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IAuditService
            _auditServiceMock = new Mock<IAuditService>();

            // AuditLogHelper is not used by GetAuditById, so we pass null!
            _controller = new AuditController(
                _auditServiceMock.Object,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetAuditById_AuditExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake audit
            var fakeAudit = new AuditResponseDTO { AuditID = 1 };
            _auditServiceMock.Setup(s => s.GetAuditByIdAsync(1)).ReturnsAsync(fakeAudit);

            // Act
            var result = await _controller.GetAuditById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetAuditById_AuditDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _auditServiceMock.Setup(s => s.GetAuditByIdAsync(99)).ReturnsAsync((AuditResponseDTO)null!);

            // Act
            var result = await _controller.GetAuditById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
