using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TalentSphere.Controllers;
using TalentSphere.DTOs;
using TalentSphere.Services.Interfaces;

namespace TalentSphere.Tests.Controllers
{
    // Tests for ResumeController.GetById endpoint
    [TestFixture]
    public class ResumeControllerTests
    {
        private Mock<IResumeService> _resumeServiceMock;
        private ResumeController _controller;

        [SetUp]
        public void Setup()
        {
            // Create a fake IResumeService
            _resumeServiceMock = new Mock<IResumeService>();

            // FileStorage and AuditLogHelper are not used by GetById, so pass null!
            _controller = new ResumeController(
                _resumeServiceMock.Object,
                fileStorage: null!,
                auditLogHelper: null!);
        }

        [Test]
        public async Task GetById_ResumeExists_ReturnsOkResult()
        {
            // Arrange: mock returns a fake resume
            var fakeResume = new ResumeResponseDTO { ResumeID = 1 };
            _resumeServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(fakeResume);

            // Act
            var result = await _controller.GetById(1);

            // Assert: 200 OK
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetById_ResumeDoesNotExist_ReturnsNotFound()
        {
            // Arrange: mock returns null
            _resumeServiceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((ResumeResponseDTO?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert: 404 Not Found
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
